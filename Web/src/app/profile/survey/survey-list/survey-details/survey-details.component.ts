import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { ControlContainer, FormControl, FormGroup } from '@angular/forms';
import { Answer } from 'src/app/models/answer';
import { Comments } from 'src/app/models/comment';
import { Question } from 'src/app/models/question';
import { Survey } from 'src/app/models/survey';
import { SurveyQuestion } from 'src/app/models/surveyQuestion';
import { ProfileService } from 'src/app/profile/profile.service';

@Component({
  selector: 'app-survey-details',
  templateUrl: './survey-details.component.html',
  styleUrls: ['./survey-details.component.scss']
})
export class SurveyDetailsComponent implements OnInit, OnDestroy {

  @Input() public Id!:string;
  public activeQuestions:Question[]=[];
  public currentSurveyId!:string;
  public formAnswer!:FormGroup;
  constructor(public readonly profileService:ProfileService,private datePipe: DatePipe) { }
  ngOnDestroy(): void {
  }


  ngOnInit(): void {
    this.getQuestionBySurveyId(this.Id);
    var d=Date.now()
    console.log(this.datePipe.transform(d,"yyyy-MM-ddThh:mm:ss"))
  }

  public getQuestionBySurveyId(id:string){
    this.profileService.getQuestionsBySurveyId(id).subscribe(
      (data)=>{
        console.log("data:",data);
        this.currentSurveyId=data[0].idSurvey;
        data.forEach(element=>{
          this.profileService.getQuestionById(element.idQuestion).subscribe(
            (data)=>{
              var question=new Question(data.questions);
              question.id=data.id;
              this.activeQuestions.push(question);
            }
          )
        });
        this.initializeForm(data);
      },
      (error)=>{console.log("error:",error)}
    )
  }

  public initializeForm(questions:SurveyQuestion[]){

    const group: any = {};

    for(let i=0;i<questions.length;++i){
      //this.formAnswer.addControl(i.toString(),new FormControl(null))
      // group.add(new FormControl(null));
      group[`ans${questions[i].idQuestion}`]=new FormControl(null);
    }
    group["comment"]=new FormControl(null);
    this.formAnswer=new FormGroup(group);

    //
    this.CheckIfIsComplete(this.profileService.currentIdUser,questions)
    //

  }

  public CheckIfIsComplete(idUser:string, questions:SurveyQuestion[]):void{
    this.profileService.GetAnswersByUserIdAndSurveyId(idUser, questions[0].idSurvey).subscribe(
      (data)=>{
        if(data.length>0){
          for(let i=0;i<data.length;++i){
            this.formAnswer.get(`ans${questions[i].idQuestion}`)?.setValue(data[i].agree)
          }
        };
        this.profileService.GetCommentByUserIdAndSurveyId(idUser,questions[0].idSurvey).subscribe(
          (data)=>{
            if(data!=null){
              this.formAnswer.get("comment")?.setValue(data.text);
            }
          }
        )
      }
    )
  }

  public submitAnswer():void{
    for(let i=0;i<this.activeQuestions.length;++i){
      console.log("index:",this.formAnswer.get(`ans${i}`)?.value);
      var yesNo:boolean;
      if(this.formAnswer.get(`ans${this.activeQuestions[i].id}`)?.value){
        yesNo=true;
      }else{
        yesNo=false;
      }
      console.log("yesno:",yesNo);
      var answer=new Answer(
        yesNo,
        this.datePipe.transform(Date.now(),"yyyy-MM-ddThh:mm:ss")??"null",
        this.profileService.currentIdUser,
        this.currentSurveyId,
        this.activeQuestions[i].id??"null"
      );
      this.profileService.CreateAnswer(answer).subscribe(
        (data)=>{console.log("answ:",data)},
        (error)=>{console.log(error)}
      )
    }
    if(this.formAnswer.get("comment")?.value){
      var comment=new Comments(this.formAnswer.get("comment")?.value,this.profileService.currentIdUser,this.currentSurveyId)
      this.profileService.CreateComment(comment).subscribe()
    }
  }
  public getCheckBoxName(index?:string){
    return `ans${index}`;
  }

  public back():void{
    this.activeQuestions=[];
  }
}
