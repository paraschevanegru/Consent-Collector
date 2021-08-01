import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../models/user';
import { UserDetail } from '../models/userDetail';
import { ProfileService } from './profile.service';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public nrOfNotification=13;
  public userId!:string;
  public user!:User;
  public userDetail!:UserDetail;

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
