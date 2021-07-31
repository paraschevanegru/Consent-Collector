import { Component, OnInit, Output } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { User } from 'src/app/models/user';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-add-admin',
  templateUrl: './add-admin.component.html',
  styleUrls: ['./add-admin.component.scss']
})
export class AddAdminComponent implements OnInit {
  displayAdmin: boolean = false;
  public formAddAdmin!: FormGroup;
  @Output()
  public listOfUsers: User[] = [];
  constructor(private adminService: AdminService) { }
  initalizeFormGroup(): void {
    this.formAddAdmin = new FormGroup({
      role: new FormControl(null),
      listOfUsers: new FormArray([]),
    })
  }
  ngOnInit(): void {
    this.adminService.toggleAddAdmin.subscribe(status => this.displayAdmin = status);
    this.initalizeFormGroup();
    this.returnAllUsers();
  }

  private returnAllUsers(): void {
    this.adminService.getAllUsers().subscribe(
      (data) => {
        data.forEach(user => {
          this.listOfUsers.push(user);
        })
      },
      (error) => {
        console.log("error:", error);
      }
    )
  }

  public submitAddAdmin(): void {
   console.log("hey");
  }

}
