import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { combineAll } from 'rxjs/operators';
import { Question } from 'src/app/models/question';
import { Survey } from 'src/app/models/survey';
import { ProfileService } from '../../profile.service';

@Component({
  selector: 'app-survey-list',
  templateUrl: './survey-list.component.html',
  styleUrls: ['./survey-list.component.scss']
})
export class SurveyListComponent implements OnInit {
  public survey!:Survey[];
  public surveyIsOpen:boolean=false;
  public surveyId!:string;
  constructor(private readonly profileService:ProfileService) {}

  ngOnInit(): void {
    this.profileService.getAllSurveys().subscribe(
      (data)=>{
        //console.log(data);
        this.survey=data;
        //console.log(this.survey);
      }
    )
  }
  public open(id?:string):void{
    console.log("id:",id);
    //var question:Question;
    this.surveyId=id?id:"null";
    this.surveyIsOpen=true;
    // this.profileService.getQuestionsBySurveyId(id).subscribe(
    //   (data)=>{
    //     console.log("data:",data);
    //     data.forEach(element=>{
    //       this.profileService.getQuestionById(element.idQuestion).subscribe(
    //         (data)=>{
    //           question=new Question(data.questions);
    //           question.id=data.id;
    //           this.profileService.activeQuestions.push(question);
    //         }
    //       )
    //     });
    //     this.profileService.isOpen=true;
    //     this.surveyIsOpen=true;

    //   },
    //   (error)=>{console.log("error:",error)}
    // )
  }
}
