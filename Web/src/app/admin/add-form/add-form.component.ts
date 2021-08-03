import { SurveyQuestion } from './../../models/surveyQuestion';
import { LoginComponent } from './../../authentication/login/login.component';
import { Survey } from 'src/app/models/survey';
import { AdminService } from './../admin.service';
import { Component, OnInit, Output } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Question } from 'src/app/models/question';
import { Notifications } from 'src/app/models/notification';
import { Historys } from 'src/app/models/history';

@Component({
  selector: 'app-add-form',
  templateUrl: './add-form.component.html',
  styleUrls: ['./add-form.component.scss']
})

export class AddFormComponent implements OnInit {
  public alertPopupAddSurvey: boolean = false;
  public dangerPopupAddSurvey: boolean = false;
  display: boolean = false;
  displayRefreshedTable: boolean = false;
  public formAddSurvey!: FormGroup;
  public formAddNewQuestion!: FormGroup;
  @Output()
  public survey!: Survey;
  currentDate: Date = new Date();
  public listOfQuestions: Question[] = [];


  constructor(private adminService: AdminService) {
  }
  initalizeFormGroup(): void {
    this.formAddSurvey = new FormGroup({
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
    })
  }

  initalizeQuestionFormGroup(): void {
    this.formAddNewQuestion = new FormGroup({
      optional: new FormControl(false),
      questions: new FormControl(null),
    })
  }
  ngOnInit(): void {
    this.adminService.toggleAddNewSurvey.subscribe(status => this.display = status);
    this.initalizeFormGroup();
    this.initalizeQuestionFormGroup();
    this.returnAllQuestions();
  }
  close() {
    this.alertPopupAddSurvey = false;
    this.dangerPopupAddSurvey = false;
  }
  public submitAddNewQuestion(): void {
    let questionData = this.formAddNewQuestion.value;
    this.adminService.postQuestion(questionData).subscribe(
      (result) => {
        this.listOfQuestions.push({ id: result.id, optional: questionData.optional, questions: questionData.questions });
        var history=`Created new question with id [${result.id}]`;
        this.adminService.CreateHistory(new Historys(history)).subscribe();
      }
    );
  }
  onCheckChange(event: any) {
    const formArray: FormArray = this.formAddSurvey.get('listOfQuestions') as FormArray;
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
  private returnAllQuestions(): void {
    this.adminService.getAllQuestions().subscribe(
      (data) => {
        data.forEach(question => {
          this.listOfQuestions.push(question);
        })
      },
      (error) => {
        console.log("error:", error);
      }
    )
  }
  public submitAddSurvey(): void {
    let consentData = this.formAddSurvey.value;
    let questions = this.formAddSurvey.value.listOfQuestions;
    // delete consentData.listOfQuestions;
    console.log("Survey:", JSON.stringify(consentData));
    this.adminService.postConsent(consentData).subscribe(
      (data) => {
        var surveyName=data.subject;
        var surveyId=data.id;
        var history=`Created new survey with id [${surveyId}]`;
        this.adminService.CreateHistory(new Historys(history)).subscribe();
        this.adminService.getAllUsers().subscribe(
          (data)=>{
            data.forEach(el=>{
              if(el.role=="user"){
                var notification=new Notifications("New survey",`Survey: ${surveyName} has been created`,el.id??"null",surveyId??"null",false);
                this.adminService.CreateNotification(notification).subscribe();
              }
            })
          }
        )

        console.log(data);
        questions.forEach((question: any) => {
          let surveyMap: any = { idSurvey: data.id, idQuestion: question }
          this.adminService.postSurveyQuestion(surveyMap).subscribe(
            (result) => {
              console.log(result);
              this.adminService.refreshTableContent(true);
            }
          );
        });
        this.alertPopupAddSurvey = true;
      },
      (error) => {
        console.log("error:", error);
        this.dangerPopupAddSurvey = true;
      }
    )
  }
}
