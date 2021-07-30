export class Survey{
  id?: string;
  subject:string;
  description:string;
  legalBasis:string;
  launchDate:string;
  expirationDate:string;
  public constructor(subject:string, description:string, legalBasis:string, launchDate:string, expirationDate:string){
    this.subject=subject;
    this.description=description;
    this.legalBasis=legalBasis;
    this.launchDate=launchDate;
    this.expirationDate=expirationDate;
  }
}
