import { AdminService } from './../admin.service';
import { Component, OnInit } from '@angular/core';
import { Survey } from 'src/app/models/survey';

@Component({
  selector: 'app-survey-admin-list',
  templateUrl: './survey-admin-list.component.html',
  styleUrls: ['./survey-admin-list.component.scss']
})
export class SurveyAdminListComponent implements OnInit {
  public survey!:Survey[];
  public surveyIsOpen:boolean=false;
  public surveyId!:string;
  constructor(private readonly adminService:AdminService) { }

  ngOnInit(): void {
    this.adminService.getAllSurveys().subscribe(
      (data)=>{
        this.survey=data;
      }
    )
  }

  public deleteSurvey(id?:string):void{
    console.log("id:",id);
    this.adminService.deleteBySurveyId(id).subscribe(
      () => {
        console.log("delete from SurveyQuestion");
      }
    );
    this.adminService.deleteSurvey(id).subscribe(
      () => {
        console.log("delete from Survey");
      }
    );;
  }

}
