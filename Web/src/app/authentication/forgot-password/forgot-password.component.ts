import { Component, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { UserDetail } from 'src/app/models/userDetail';
import { ForgotPasswordService } from './forgot-password.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
  @Input()
  public formPassword!: FormGroup;
  public formNewPassword!: FormGroup;
  public validare:boolean=false;
  public invalidCredentials:boolean=false;
  public error:boolean=false;
  @Output()
  public userDetail!:UserDetail;
  public user!:User;

  constructor(private readonly forgot_passwordService:ForgotPasswordService,public readonly router:Router) { }

  ngOnInit(): void {
    this.initializeFormGroup();
  }

  initializeFormGroup():void{
    this.formPassword=new FormGroup({
      register_email:new FormControl(null,[
        Validators.required,
        Validators.email
      ]),
      register_phone:new FormControl(null,[
        Validators.required,
        Validators.maxLength(10),
        Validators.minLength(10),
        Validators.pattern("[0-9]{10}")
      ])
    })
    this.formNewPassword=new FormGroup({
      register_password1:new FormControl(null,[
        Validators.required,
        Validators.maxLength(16),
        Validators.minLength(8),
        Validators.pattern("^(?=(.*[A-Z]){1})(?=(.*[a-z]){1})(?=(.*[0-9]){1})(?=(.*[@#$%^!&+=.\-_*]){2})([a-zA-Z0-9@#$%^!&+=*.\-_]){8,16}")
      ]),
      register_password2:new FormControl(null,[
        Validators.required,
        Validators.maxLength(16),
        Validators.minLength(8),
        Validators.pattern("^(?=(.*[A-Z]){1})(?=(.*[a-z]){1})(?=(.*[0-9]){1})(?=(.*[@#$%^!&+=.\-_*]){2})([a-zA-Z0-9@#$%^!&+=*.\-_]){8,16}")
      ])
    })
  }

  verify()
  {
    console.log("ForgotPassword:",JSON.stringify(this.formPassword.value));
    this.forgot_passwordService.getUserDetail(this.formPassword.value.register_email,this.formPassword.value.register_phone).subscribe(
      (data) => {
        this.userDetail=data;
        this.validare=true;
      },
      (error)=>{
        console.log("error:",error);
        this.invalidCredentials=true;
        this.router.navigateByUrl('/authentication/forgot_password');
        this.error=true;
      }
    )
  }

  redirectToLogin(){
    this.router.navigateByUrl('/authentication/login');
  }

  submitNewPassword()
  {
    this.forgot_passwordService.getUser(this.userDetail.idUser!).subscribe(
      (data) => {
        var user=new User(data.username, this.formNewPassword.value.register_password1);
        this.forgot_passwordService.postNewPassword(data.id??"null", user).subscribe();
      }
    )
    this.validare=false;
    this.redirectToLogin();
  }
}
