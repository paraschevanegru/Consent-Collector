import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Survey } from 'src/app/models/survey';
import { ProfileService } from 'src/app/profile/profile.service';

@Component({
  selector: 'app-survey-details',
  templateUrl: './survey-details.component.html',
  styleUrls: ['./survey-details.component.scss']
})
export class SurveyDetailsComponent implements OnInit {

  constructor(public readonly profileService:ProfileService) { }

  ngOnInit(): void {
  }

  public back():void{
    this.profileService.isOpen=false;
    this.profileService.activeQuestions=[];
  }
}
