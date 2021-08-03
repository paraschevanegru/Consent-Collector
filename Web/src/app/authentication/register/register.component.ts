import { JsonpClientBackend } from '@angular/common/http';
import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/models/user';
import { UserDetail } from 'src/app/models/userDetail';
import { RegisterService } from './register.service';
import {concatMap} from 'rxjs/operators';
import { Router } from '@angular/router';
import { Historys } from 'src/app/models/history';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit, OnChanges, OnDestroy {
  @Input()
  public invalidCredentials:boolean=false;
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
    this.initializeFormGroup();
  }

  initializeFormGroup():void{
    this.formRegister=new FormGroup({
      register_firstName:new FormControl(null,[
        Validators.required,
        Validators.maxLength(30),
        Validators.minLength(3)
      ]),
      register_lastName:new FormControl(null,[
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(30)
      ]),
      register_email:new FormControl(null,[
        Validators.required,
        Validators.email
      ]),
      register_phone:new FormControl(null,[
        Validators.required,
        Validators.maxLength(10),
        Validators.minLength(10),
        Validators.pattern("[0-9]{10}")
      ]),
      register_username:new FormControl(null,[
        Validators.required,
        Validators.maxLength(20),
        Validators.minLength(3),
        Validators.pattern("^[a-zA-Z0-9]*$")
      ]),
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

  redirectToLogin(){
    this.router.navigateByUrl('/authentication/login');
  }

  public submitRegister():void{
    console.log(`Register submit: ${JSON.stringify(this.formRegister.value)}`);
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
          (data)=>{
            var history=`A new user with id [${this.user.id}] has been registered`;
            this.registerService.CreateHistory(new Historys(history)).subscribe();
            this.redirectToLogin()
          },
          (error)=>{
            this.registerService.deleteUser(this.user).subscribe()
            this.invalidCredentials=true;
          }
        );
  }
}
