export class Comments{
  id?:string;
  text:string;
  idUser:string;
  idSurvey:string;
  public constructor(text:string,idUser:string,idSurvey:string){
    this.text=text;
    this.idUser=idUser;
    this.idSurvey=idSurvey;
  }
}
