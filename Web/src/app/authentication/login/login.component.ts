import { Component, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { UserDetail } from 'src/app/models/userDetail';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public formLogin!:FormGroup;
  @Output()
  public user!:User;
  @Output()
  public userDetail!:UserDetail;
  @Input()
  public invalidCredentials:boolean=false;

  constructor(private readonly loginService:LoginService,public readonly router:Router) { }

  ngOnInit(): void {
    this.initalizeFormGroup();
  }

  initalizeFormGroup():void{
    this.formLogin=new FormGroup({
      username:new FormControl(null),
      password:new FormControl(null)
    })
  }
  public submitLogin():void{
    this.loginService.getUser(new User(this.formLogin.value.username,this.formLogin.value.password)).subscribe(
      (data)=>{
        console.log("role:", data.role);
        this.user=data,
        this.loginService.getUserDetail(this.user).subscribe(
          (data)=>{
            this.userDetail=data;
            console.log(data)
            if(this.user.role=="user"){
              this.router.navigateByUrl(`/profile/${this.user.id}`);
            }else if(this.user.role=="admin"){
              this.router.navigateByUrl(`/admin/${this.user.id}`);
            }
          }
        )
      },
      (error)=>{
        console.log("error:",error),
        this.invalidCredentials=true;
      }
    )
  }

}
