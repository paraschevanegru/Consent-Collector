export class Question{
  id?:string;
  optional?:boolean;
  questions:string;
  public constructor(questions:string){
    this.questions=questions;
  }
}
