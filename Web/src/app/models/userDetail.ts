export class UserDetail{
  id?:string;
  idUser?:string="";
  firstname:string;
  lastname:string;
  email:string;
  number:string;

  public constructor(idUser:string,firstname:string,lastname:string,email:string,phone:string){
    this.idUser=idUser;
    this.firstname=firstname;
    this.lastname=lastname;
    this.email=email;
    this.number=phone;
  }
}
