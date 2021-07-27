import { Component, OnInit } from '@angular/core';
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
  public user!:User;
  public userDetail!:UserDetail;

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
    console.log("Login:",JSON.stringify(this.formLogin.value));
    this.loginService.getUser(new User(this.formLogin.value.username,this.formLogin.value.password)).subscribe(
      (data)=>{
        this.user=data,
        this.loginService.getUserDetail(this.user).subscribe(
          (data)=>{
            this.userDetail=data;
            console.log(data)
            this.router.navigateByUrl('/profile');
          }
        )
      },
      (error)=>{console.log("error:",error)}
    )
  }

}
