import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { ControlContainer, FormControl, FormGroup } from '@angular/forms';
import { Answer } from 'src/app/models/answer';
import { Comments } from 'src/app/models/comment';
import { Historys } from 'src/app/models/history';
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
  public checkComment=false;
  public checkSurvey=false;
  public idComment!:string;
  public idAnswers:string[]=[];
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
      group[`ans${questions[i].idQuestion}`]=new FormControl(null);
    }
    group["comment"]=new FormControl(null);
    this.formAnswer=new FormGroup(group);

    this.CheckIfIsComplete(this.profileService.currentIdUser,questions)
  }

  public CheckIfIsComplete(idUser:string, questions:SurveyQuestion[]):void{
    this.profileService.GetAnswersByUserIdAndSurveyId(idUser, questions[0].idSurvey).subscribe(
      (data)=>{

        if(data.length>0){
          this.checkSurvey=true;
          for(let i=0;i<data.length;++i){
            this.idAnswers[i]=data[i].id??"null";

            var dataFind=data.find((x)=>{
              x.idQuestion==questions[i].idQuestion;
            });
            //this.formAnswer.get(`ans${questions[i].idQuestion}`)?.setValue(dataFind?.agree);
            this.formAnswer.get(`ans${data[i].idQuestion}`)?.setValue(data[i].agree);
          }
        };
        this.profileService.GetCommentByUserIdAndSurveyId(idUser,questions[0].idSurvey).subscribe(
          (data)=>{
            if(data!=null){
              this.checkComment=true;
              this.idComment=data.id??"null";
              this.formAnswer.get("comment")?.setValue(data.text);
            }
          }
        )
      }
    )
  }

  public submitAnswer():void{
    var idQuestion:string;
    var idComment:string;
    if(this.formAnswer.get("comment")?.value){
      var comment=new Comments(this.formAnswer.get("comment")?.value,this.profileService.currentIdUser,this.currentSurveyId)
      if(this.checkComment){
        this.profileService.UpdateComment(this.idComment,comment).subscribe(
          (data)=>{
            var history=`User with id [${this.profileService.currentIdUser}] add comment with id [${idComment}] to the survey with id [${this.currentSurveyId}]`;
            this.profileService.CreateHistory(new Historys(history)).subscribe();
          }
        );
      }else{
        this.profileService.CreateComment(comment).subscribe(
          (data)=>{
            idComment=data.id??"null";
            var history=`User with id [${this.profileService.currentIdUser}] updated comment with id [${data.id}] to the survey with id [${this.currentSurveyId}]`;
            this.profileService.CreateHistory(new Historys(history)).subscribe();
          }
        )
      }
    }

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
      )
      if(this.checkSurvey){
        this.profileService.UpdateAnswer(this.idAnswers[i],answer).subscribe(
          (data)=>{
            var history=`User with id [${this.profileService.currentIdUser}] updated question with id [${idQuestion}] in the survey with id [${this.currentSurveyId}]`;
            this.profileService.CreateHistory(new Historys(history)).subscribe();
            window.location.reload();
          }
        );
      }
      else{
        this.profileService.CreateAnswer(answer).subscribe(
          (data)=>{
            idQuestion=data.id??"null";
            var history=`User with id [${this.profileService.currentIdUser}] answered question with id [${data.id}] in the survey with id [${this.currentSurveyId}]`;
            this.profileService.CreateHistory(new Historys(history)).subscribe();
            window.location.reload();
          },
          (error)=>{console.log(error)}
        )
      }

    }
  }
  public getCheckBoxName(index?:string){
    return `ans${index}`;
  }

  public back():void{
    window.location.reload();
  }
}
