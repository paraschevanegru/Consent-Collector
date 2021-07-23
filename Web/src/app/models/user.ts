export class User{
  id?: string;
  username:string;
  password:string;
  role:string;
  public constructor(username:string,password:string){
    this.username=username;
    this.password=password;
    this.role="user";
  }
}
