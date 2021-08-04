export class Notifications{
  id?:string;
  title: string;
  description: string;
  idUser: string;
  idSurvey:string;
  seen:boolean;
  public constructor(title:string, description:string,idUser:string,idSurvey:string, seen:boolean){
    this.title=title;
    this.description=description;
    this.idUser=idUser;
    this.idSurvey=idSurvey;
    this.seen=seen;
  }
}
