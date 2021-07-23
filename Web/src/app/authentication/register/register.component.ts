import { JsonpClientBackend } from '@angular/common/http';
import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { User } from 'src/app/models/user';
import { UserDetail } from 'src/app/models/userDetail';
import { RegisterService } from './register.service';
import {concatMap} from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit, OnChanges, OnDestroy {

  //registerService:RegisterService;

  public formRegister!: FormGroup;
  public user!:User;
  public user_detail!: UserDetail;
  constructor(private readonly registerService:RegisterService,private readonly router:Router) {
  }

  ngOnDestroy(): void {
    //throw new Error('Method not implemented.');
  }
  ngOnChanges(changes: SimpleChanges): void {
    //throw new Error('Method not implemented.');
  }
  //constructor() { }



  ngOnInit(): void {
    this.formRegister=new FormGroup({
      register_firstName:new FormControl(null),
      register_lastName:new FormControl(null),
      register_email:new FormControl(null),
      register_phone:new FormControl(null),
      register_username:new FormControl(null),
      register_password1:new FormControl(null),
      register_password2:new FormControl(null)
    })
  }

  redirectToLogin(){
    this.router.navigateByUrl('/authentication/login');
  }

  public submitRegister():void{
    //console.log(`Register submit: ${JSON.stringify(this.formRegister.value)}`);
    this.user=new User(this.formRegister.value.register_username,this.formRegister.value.register_password1);
    this.user_detail=new UserDetail("null" , this.formRegister.value.register_firstName,this.formRegister.value.register_lastName,this.formRegister.value.register_email,this.formRegister.value.register_phone);

    this.registerService.createUser(this.user).pipe(concatMap((data)=>{
      console.log("data:",data);
      console.log("register value:",this.formRegister.value);
      this.user.id=data.id;
      this.user_detail.idUser=this.user.id;
      console.log(this.user_detail);

      return this.registerService.createDetailUser(this.user_detail);
    })).subscribe(
          (data)=>console.log("end:",data),
          (error)=>{
            console.log()
            this.registerService.deleteUser(this.user)}
          );
  }
}
