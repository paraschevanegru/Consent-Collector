import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
        this.survey=data;
      }
    )
  }
  public open(id?:string):void{
    console.log("id:",id);
    this.surveyId=id?id:"null";
    this.surveyIsOpen=true;
  }
}
