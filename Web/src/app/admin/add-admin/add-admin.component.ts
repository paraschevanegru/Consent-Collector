import { LEADING_TRIVIA_CHARS } from '@angular/compiler/src/render3/view/template';
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
  public listOfNewAdmins: string[] = [];
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
          if (user.role == "user")
            this.listOfUsers.push(user);
        })
      },
      (error) => {
        console.log("error:", error);
      }
    )
  }

  public submitAddAdmin(): void {
    console.log(this.listOfNewAdmins);
    const formArray: FormArray = this.formAddAdmin.get('listOfUsers') as FormArray;
    this.listOfNewAdmins.forEach(user => {
      this.adminService.patchUserRole(user, "admin").subscribe(
        (data) => {
          console.log("update to Admin");
          const indx = this.listOfUsers.findIndex(u => u.id == data.id);
          this.listOfUsers.splice(indx, indx >= 0 ? 1 : 0);
        }
      );

    });
  }
  private removeItem<T>(arr: Array<T>, value: T): Array<T> {
    const index = arr.indexOf(value);
    if (index > -1) {
      arr.splice(index, 1);
    }
    return arr;
  }
  onCheckChange(event: any) {

    if (event.target.checked) {
      console.log(event.target.value)
      this.listOfNewAdmins.push(event.target.value);
    }
    else {
      this.removeItem(this.listOfNewAdmins, event.target.value);
    }
  }

}
