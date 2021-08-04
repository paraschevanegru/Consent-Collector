import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
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
  currentDate: Date = new Date();
  public formSubmitFilter!: FormGroup;
  public surveyIsOpen:boolean=false;
  public surveyId!:string;
  constructor(private readonly profileService:ProfileService) {}
  initalizeFormGroup(): void {
    this.formSubmitFilter = new FormGroup({
      launchDate: new FormControl(null),
      expirationDate: new FormControl(null),
      legalBasis : new FormControl(null)

    })
  }
  ngOnInit(): void {
    this.profileService.getAllSurveys().subscribe(
      (data)=>{
        this.survey=data;
      }
    )
    this.initalizeFormGroup();
  }
  public open(id?:string):void{
    console.log("id:",id);
    this.surveyId=id?id:"null";
    this.surveyIsOpen=true;
  }
  public isExpirate(date:string):boolean{
    return Date.parse(date)>Date.now();
  }
  public submitFilter():void{
    var filter = this.formSubmitFilter.value;
    console.log(filter);
    this.profileService.getAllSurveys(filter.launchDate,filter.expirationDate, filter.legalBasis).subscribe(
      (data)=>{
        console.log(data);
        this.survey = data;
      }
    )
  }

}
