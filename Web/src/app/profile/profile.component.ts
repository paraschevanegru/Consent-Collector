import { Component, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Notifications } from '../models/notification';
import { User } from '../models/user';
import { UserDetail } from '../models/userDetail';
import { ProfileService } from './profile.service';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public nrOfNotification=0;
  public userId!:string;
  public user!:User;
  public userDetail!:UserDetail;
  public notifications:Notifications[]=[];
  public displayNotifications=false;

  constructor(public readonly router:Router,private activatedRoute: ActivatedRoute,private readonly profileService:ProfileService) {
    this.activatedRoute.params.subscribe(params => {
      this.userId= params['id'];
      this.profileService.getUser(this.userId).subscribe(
        (data)=>{
          this.user=data;
          this.profileService.getUserDetail(this.userId).subscribe(
            (data)=>{
              this.userDetail=data;
              this.profileService.currentIdUser=this.user.id?this.user.id:"null";
              this.profileService.GetAllNotificationOfUser(this.user.id).subscribe(
                (data)=>{
                  this.nrOfNotification=data.length;
                  this.notifications=data;
                }
              );
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

  public clickNotification():void{
    this.displayNotifications=!this.displayNotifications;
  }

  public decrementNotification(n:any){
    this.nrOfNotification-=n;
    console.log("event:",n);
  }

}
