import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ControlContainer, FormControl, FormGroup } from '@angular/forms';
import { Question } from 'src/app/models/question';
import { Survey } from 'src/app/models/survey';
import { ProfileService } from 'src/app/profile/profile.service';

@Component({
  selector: 'app-survey-details',
  templateUrl: './survey-details.component.html',
  styleUrls: ['./survey-details.component.scss']
})
export class SurveyDetailsComponent implements OnInit {

  @Input() public Id!:string;
  public activeQuestions:Question[]=[];
  public formAnswer!:FormGroup;
  constructor(public readonly profileService:ProfileService) { }


  ngOnInit(): void {
    this.getQuestionBySurveyId(this.Id);
  }

  public getQuestionBySurveyId(id:string){
    this.profileService.getQuestionsBySurveyId(id).subscribe(
      (data)=>{
        console.log("data:",data);
        data.forEach(element=>{
          this.profileService.getQuestionById(element.idQuestion).subscribe(
            (data)=>{
              var question=new Question(data.questions);
              question.id=data.id;
              this.activeQuestions.push(question);
            }
          )
        });
        this.initializeForm(data.length);
      },
      (error)=>{console.log("error:",error)}
    )
  }

  public initializeForm(numberOfQuestions:number){

    const group: any = {};

    for(let i=0;i<numberOfQuestions;++i){
      //this.formAnswer.addControl(i.toString(),new FormControl(null))
      // group.add(new FormControl(null));
      group[`ans${i}`]=new FormControl(null);
    }
    this.formAnswer=new FormGroup(group);

      // const group: any = {};
      // this.profileService.activeQuestions.forEach(question => {
      //   group[question.id??question.id] = question.required ? new FormControl(question.value || '', Validators.required)
      //                                           : new FormControl(question.value || '');
      // });
      // return new FormGroup(group);

  }

  public submitAnswer():void{
    for(let i=0;i<this.activeQuestions.length;++i){
      console.log("index:",this.formAnswer.get(`ans${i}`)?.value);
    }
  }
  public getCheckBoxName(index:string){
    return `ans${index}`;
  }

  // public back():void{
  //   this.profileService.isOpen=false;
  //   this.profileService.activeQuestions=[];
  // }
}
