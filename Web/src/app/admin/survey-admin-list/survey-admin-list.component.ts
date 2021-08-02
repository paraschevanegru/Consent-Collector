import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from './../admin.service';
import { Component, OnInit } from '@angular/core';
import { Survey } from 'src/app/models/survey';

@Component({
  selector: 'app-survey-admin-list',
  templateUrl: './survey-admin-list.component.html',
  styleUrls: ['./survey-admin-list.component.scss']
})
export class SurveyAdminListComponent implements OnInit {
  public alertPopup: boolean = false;
  pOpened:boolean = false;
  public survey!: Survey[];
  surveyId!: string;
  currentSurvey!: string;
  displayEditSurvey: boolean = false;
  constructor(private readonly adminService: AdminService) { }

  ngOnInit(): void {
    this.adminService.getAllSurveys().subscribe(
      (data) => {
        this.survey = data;
      }
    );
  }

  close() {
    this.alertPopup = false;
  }
  onPressEditSurvey(id?: string) {
    this.displayEditSurvey = !this.displayEditSurvey;
    this.adminService.showEditSurvey(this.displayEditSurvey);
    if (id != undefined) {
      this.surveyId = id;
      this.currentSurvey = id;
      this.adminService.shareSurveyId(this.surveyId);
    }

  }
  public deleteSurvey(id?: string): void {
    console.log("id:", id);
    this.adminService.deleteBySurveyId(id).subscribe(
      (data) => {
        console.log("delete from SurveyQuestion");
        this.alertPopup = true;
        window.location.reload();
      }

    );
    this.adminService.deleteSurvey(id).subscribe(
      () => {
        console.log("delete from Survey");
      }
    );
  }

}
