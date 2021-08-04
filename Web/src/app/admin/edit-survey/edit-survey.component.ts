import { Survey } from './../../models/survey';
import { Component, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Question } from 'src/app/models/question';
import { AdminService } from '../admin.service';
import { Notifications } from 'src/app/models/notification';
import { combineAll } from 'rxjs/operators';
import { Historys } from 'src/app/models/history';

@Component({
  selector: 'app-edit-survey',
  templateUrl: './edit-survey.component.html',
  styleUrls: ['./edit-survey.component.scss']
})
export class EditSurveyComponent implements OnInit {
  public alertPopupEdit: boolean = false;
  public dangerPopupEdit: boolean = false;
  displayEditSurvey: boolean = false;
  surveyId!: string;
  public formEditSurvey!: FormGroup;
  public formAddNewQuestion!: FormGroup;
  @Output()
  public survey!: Survey;
  currentDate: Date = new Date();
  public listOfQuestions: Question[] = [];
  constructor(private adminService: AdminService) { }

  initalizeFormGroup(): void {
    this.formEditSurvey = new FormGroup({
      subject: new FormControl(null,[
        Validators.required,
        Validators.maxLength(50),
        Validators.minLength(3)
      ]),
      description: new FormControl(null,[
        Validators.required,
        Validators.maxLength(150),
        Validators.minLength(3)
      ]),
      legalBasis: new FormControl(null),
      launchDate: new FormControl(null),
      expirationDate: new FormControl(null),
      listOfQuestions: new FormArray([]),
    });
  }


  initalizeQuestionFormGroup(): void {
    this.formAddNewQuestion = new FormGroup({
      optional: new FormControl(false),
      questions: new FormControl(null),
    })
  }
  ngOnInit(): void {
    this.adminService.sharedDisplayEdit.subscribe(status => this.displayEditSurvey = status);
    this.adminService.sharedMessage.subscribe(status => this.surveyId = status);

    this.initalizeFormGroup();
    this.returnSurvey(this.surveyId);
    this.initalizeQuestionFormGroup();
    this.returnQuestionsBySurveyId(this.surveyId);

  }
  close() {
    this.alertPopupEdit = false;
    this.dangerPopupEdit = false;
  }
  onCheckChange(event: any) {
    const formArray: FormArray = this.formEditSurvey.get('listOfQuestions') as FormArray;
    if (event.target.checked) {
      formArray.push(new FormControl(event.target.value));
    }
    else {
      let i: number = 0;

      formArray.controls.forEach((ctrl: any) => {
        if (ctrl.value == event.target.value) {
          formArray.removeAt(i);
          return;
        }
        i++;
      });
    }
  }

  private returnQuestionsBySurveyId(id?: string): void {
    this.adminService.getQuestionsBySurveyId(id).subscribe(
      (data) => {
        data.forEach(question => {
          this.adminService.getQuestionById(question.idQuestion).subscribe(
            (result) => {
              this.listOfQuestions.push(result);
            }
          )
        })
      },
      (error) => {
        console.log("error:", error);

      }
    )
  }
  public submitEditSurvey(): void {
    let consentData = this.formEditSurvey.value;
    let questions = this.formEditSurvey.value.listOfQuestions;
    delete consentData.listOfQuestions;
    console.log("Survey:", JSON.stringify(consentData));
    this.adminService.updateSurvey(this.surveyId, consentData).subscribe(
      (data) => {
        var history=`Updated survey with id [${this.surveyId}]`;
        this.adminService.CreateHistory(new Historys(history)).subscribe();
        this.adminService.getSurvey(this.surveyId).subscribe(
          (data)=>{
            var surveyName=data.subject;
            this.adminService.getAllUsers().subscribe(
              (data)=>{
                data.forEach(el=>{
                  if(el.role=="user"){
                    var notification=new Notifications("Update survey",`Survey: ${surveyName} has been modified`,el.id??"null",this.surveyId,false);
                    this.adminService.CreateNotification(notification).subscribe(
                      (data)=>console.log("n:",data),
                      (error)=>console.log("ne:",error)
                    );
                  }
                })
              }
            )
          }
        )
        questions.forEach((question: any) => {
          let surveyMap: any = { idSurvey: data.id, idQuestion: question }
          this.adminService.postSurveyQuestion(surveyMap).subscribe(
            (result) => {
              console.log(result);
            }
          );
        });
        this.alertPopupEdit = true;
      },
      (error) => {
        console.log("error:", error);
        this.dangerPopupEdit = true;
      }
    );

  }
  public submitAddNewQuestion(): void {
    let questionData = this.formAddNewQuestion.value;
    this.adminService.postQuestion(questionData).subscribe(
      (result) => {
        var history=`Added new question with id [${result.id}]`;
        this.adminService.CreateHistory(new Historys(history)).subscribe();
        this.listOfQuestions.push({ id: result.id, optional: questionData.optional, questions: questionData.questions });
      }
    );
  }

  private returnSurvey(id?: string): void {
    let result: Survey;
    this.adminService.getSurvey(id).subscribe(
      (data) => {
        console.log(data);
        result = data;
        var launchDate = new Date(data.launchDate).toISOString().split('T', 1)[0];
        var expirationDate = new Date(data.expirationDate).toISOString().split('T', 1)[0];
        this.formEditSurvey.setValue({
          subject: data.subject,
          description: data.description,
          legalBasis: data.legalBasis,
          launchDate: launchDate,
          expirationDate: expirationDate,
          listOfQuestions: []
        });
      }
    );

  }

}
