import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../models/user';
import { UserDetail } from '../models/userDetail';
import { AdminService } from './admin.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  public nrOfNotification=13;
  public userId!:string;
  public user!:User;
  public userDetail!:UserDetail;

  constructor(public readonly router:Router,private activatedRoute: ActivatedRoute,private readonly adminService:AdminService) {
    this.activatedRoute.params.subscribe(params => {
      this.userId= params['id'];
      this.adminService.getUser(this.userId).subscribe(
        (data)=>{
          this.user=data;
          this.adminService.getUserDetail(this.userId).subscribe(
            (data)=>{
              this.userDetail=data;
            }
          )
        },
        (error)=>{this.router.navigateByUrl("authentication/login")}
      );
    });
  }


  ngOnInit(): void {
  }

  public logout():void{
    this.router.navigateByUrl('/authentication/login');
  }

}
