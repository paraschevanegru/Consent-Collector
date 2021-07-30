export class SurveyQuestion{
  id?: string;
  idSurvey:string;
  idQuestion:string;
  public constructor(idSurvey:string, idQuestion:string){
    this.idQuestion=idQuestion;
    this.idSurvey=idSurvey;
  }
}
