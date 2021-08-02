import { DatePipe } from "@angular/common";

export class Answer{
  id?:string;
  agree: boolean;
  answerDate: string;
  idUser: string;
  idSurvey:string;
  idQuestion:string;
  public constructor(agree:boolean, answerDate:string,idUser:string,idSurvey:string, idQuestion:string){
    this.agree=agree;
    this.answerDate=answerDate;
    this.idUser=idUser;
    this.idSurvey=idSurvey;
    this.idQuestion=idQuestion;
  }
}
