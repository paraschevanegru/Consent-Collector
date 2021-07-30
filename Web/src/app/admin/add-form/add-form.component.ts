import { SurveyQuestion } from './../../models/surveyQuestion';
import { LoginComponent } from './../../authentication/login/login.component';
import { Survey } from 'src/app/models/survey';
import { AdminService } from './../admin.service';
import { Component, OnInit, Output } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { Question } from 'src/app/models/question';

@Component({
  selector: 'app-add-form',
  templateUrl: './add-form.component.html',
  styleUrls: ['./add-form.component.scss']
})

export class AddFormComponent implements OnInit {
  display:boolean = false;
  public formAddSurvey!:FormGroup;
  @Output()
  public survey!:Survey;
  currentDate : Date =new Date();
  public listOfQuestions:Question[]=[];


  constructor(private adminService:AdminService) {
  }
  initalizeFormGroup():void{
    this.formAddSurvey=new FormGroup({
      subject:new FormControl(null),
      description:new FormControl(null),
      legalBasis:new FormControl(null),
      launchDate:new FormControl(null),
      expirationDate:new FormControl(null),
      listOfQuestions: new FormArray([]),
    })
  }
  ngOnInit(): void {
    this.adminService.toggle.subscribe(status => this.display = status);
    this.initalizeFormGroup();
    this.returnAllQuestions();
  }
  onCheckChange(event: any) {
    const formArray: FormArray = this.formAddSurvey.get('listOfQuestions') as FormArray;

    /* Selected */
    if(event.target.checked){
      // Add a new control in the arrayForm
      formArray.push(new FormControl(event.target.value));
    }
    /* unselected */
    else{
      // find the unselected element
      let i: number = 0;

      formArray.controls.forEach((ctrl: any) => {
        if(ctrl.value == event.target.value) {
          // Remove the unselected element from the arrayForm
          formArray.removeAt(i);
          return;
        }

        i++;
      });
    }
  }
  private returnAllQuestions():void{
    this.adminService.getAllQuestions().subscribe(
      (data)=>{
        data.forEach(question => {
          this.listOfQuestions.push(question);
        })
      },
  (error)=>{
    console.log("error:",error);
  }
    )
  }
  public submitAddSurvey():void{
    let consentData = this.formAddSurvey.value;
    let questions = this.formAddSurvey.value.listOfQuestions;
    delete consentData.listOfQuestions;
    console.log("Survey:",JSON.stringify(consentData));
    this.adminService.postConsent(consentData).subscribe(
      (data)=>{
            console.log(data);
            questions.forEach((question: any)  => {
              let surveyMap: any = {idSurvey: data.id, idQuestion: question}
              this.adminService.postSurveyQuestion(surveyMap).subscribe(
                (result)=>{
                  console.log(result);
                }
              );
            });

      },
      (error)=>{
        console.log("error:",error);
      }
    )
  }

}
