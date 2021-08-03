import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from './../admin.service';
import { Component, OnInit } from '@angular/core';
import { Survey } from 'src/app/models/survey';
import { delay } from 'rxjs/operators';
import { Notifications } from 'src/app/models/notification';

@Component({
  selector: 'app-survey-admin-list',
  templateUrl: './survey-admin-list.component.html',
  styleUrls: ['./survey-admin-list.component.scss']
})
export class SurveyAdminListComponent implements OnInit {
  currentDate: Date = new Date();
  public formSubmitFilter!: FormGroup;
  public alertPopup: boolean = false;
  pOpened:boolean = false;
  public survey!: Survey[];
  surveyId!: string;
  currentSurvey!: string;
  displayEditSurvey: boolean = false;
  constructor(private readonly adminService: AdminService) { }
  initalizeFormGroup(): void {
    this.formSubmitFilter = new FormGroup({
      launchDate: new FormControl(null),
      expirationDate: new FormControl(null),

    })
  }
  ngOnInit(): void {
    this.adminService.getRefresh().subscribe((status: boolean) =>{
      console.log(status);
      if(status){
        this.refresh();
      }
    });
    this.refresh();
    this.initalizeFormGroup();
  }

  close() {
    this.alertPopup = false;
  }
  onPressEditSurvey(id?: string) {
    if(this.currentSurvey != id){
      this.displayEditSurvey = false;

    }
    this.displayEditSurvey = !this.displayEditSurvey;
    this.adminService.showEditSurvey(this.displayEditSurvey);
    if (id != undefined) {
      this.surveyId = id;
      this.currentSurvey = id;
      this.adminService.shareSurveyId(this.surveyId);
    }

  }
  public  deleteSurvey(id?: string): void {
    var surveyName:string;
    console.log("id:", id);
    this.adminService.getSurvey(id).subscribe((data)=>{surveyName=data.subject});
    this.adminService.deleteBySurveyId(id).subscribe(
      () => {
        console.log("delete from SurveyQuestion");
        this.adminService.deleteSurvey(id).subscribe(
          () => {
            console.log("delete from Survey");
            this.refresh();
          },
          (error) => {
            console.log("error:", error);
          }
        );
      }
    );
    this.alertPopup = true;

  }
  public refresh():void{
    this.adminService.getAllSurveys().subscribe(
      (data) => {
        console.log("refresh table");
        this.survey = data;
      }
    );
  }
public submitFilter():void{
  var filter = this.formSubmitFilter.value;
  console.log(filter);
  this.adminService.getAllSurveys(filter.launchDate,filter.expirationDate).subscribe(
    (data)=>{
      console.log(data);
      this.survey = data;
    }
  )
}
}
