import { AddFormComponent } from './add-form/add-form.component';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../models/user';
import { UserDetail } from '../models/userDetail';
import { AdminService } from './admin.service';
import { Survey } from '../models/survey';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  surveyId!: string;
  public survey!: Survey[];
  display: boolean = false;
  displayAdmin: boolean = false;
  displayEditSurvey: boolean = false;
  displayRefreshedTable: boolean = false;
  public nrOfNotification = 13;
  public userId!: string;
  public user!: User;
  public userDetail!: UserDetail;

  constructor(public readonly router: Router, private activatedRoute: ActivatedRoute, private readonly adminService: AdminService) {
    this.activatedRoute.params.subscribe(params => {
      this.userId = params['id'];
      this.adminService.getUser(this.userId).subscribe(
        (data) => {
          this.user = data;
          this.adminService.getUserDetail(this.userId).subscribe(
            (data) => {
              this.userDetail = data;
            }
          )
        },
        (error) => { this.router.navigateByUrl("authentication/login") }
      );
    });
  }


  ngOnInit(): void {
    this.adminService.toggleAddNewSurvey.subscribe(status => this.display = status);
    this.adminService.toggleAddAdmin.subscribe(status => this.displayAdmin = status);
    this.adminService.sharedDisplayEdit.subscribe(status => this.displayEditSurvey = status);
    this.adminService.sharedMessage.subscribe(status => this.surveyId = status);
    this.adminService.getRefresh().subscribe(status => this.displayRefreshedTable = status);

  }
  onPress() {
    this.adminService.toggleAddNewSurvey.emit(!this.display);
  }
  onPressAddAdmin() {
    this.adminService.toggleAddAdmin.emit(!this.displayAdmin);
  }

  public logout(): void {
    this.router.navigateByUrl('/authentication/login');
  }

}
