import { Component, OnInit, Output ,EventEmitter, Input} from '@angular/core';
import { User } from '../models/user';
//import { RegisterService } from './register/register.service';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss']
})
export class AuthenticationComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
