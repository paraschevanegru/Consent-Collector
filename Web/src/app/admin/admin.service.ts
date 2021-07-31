import { Survey } from 'src/app/models/survey';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Question } from '../models/question';
import { SurveyQuestion } from '../models/surveyQuestion';
import { User } from '../models/user';
import { UserDetail } from '../models/userDetail';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  toggleAddNewSurvey: EventEmitter<boolean> = new EventEmitter<boolean>()
  toggleAddAdmin: EventEmitter<boolean> = new EventEmitter<boolean>()
  private api: string = 'https://localhost:44311/api/v1';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  constructor(private readonly httpClient: HttpClient) {
  }
  public postConsent(data: JSON): Observable<Survey> {
    return this.httpClient.post<Survey>(`${this.api}/consent`, data, this.httpOptions);
  }
  public postSurveyQuestion(data: JSON): Observable<SurveyQuestion> {
    return this.httpClient.post<SurveyQuestion>(`${this.api}/surveyQuestion`, data, this.httpOptions);
  }
  public postQuestion(data: JSON): Observable<Question> {
    return this.httpClient.post<Question>(`${this.api}/question`, data, this.httpOptions);
  }

  public getAllUsers(): Observable<User[]> {
    return this.httpClient.get<User[]>(`${this.api}/user`, this.httpOptions);
  }

  public updateUser(idUser: string): Observable<User> {
    return this.httpClient.put<User>(`${this.api}/user/${idUser}`, this.httpOptions);
  }
  public getUser(idUser: string): Observable<User> {
    return this.httpClient.get<User>(`${this.api}/user/${idUser}`, this.httpOptions);
  }
  public getUserDetail(idUser: string): Observable<UserDetail> {
    return this.httpClient.get<UserDetail>(`${this.api}/detail/user/${idUser}`, this.httpOptions);
  }

  public getAllSurveys(): Observable<Survey[]> {
    return this.httpClient.get<Survey[]>(`${this.api}/consent`, this.httpOptions);
  }

  public getSurvey(id?: string): Observable<Survey> {
    return this.httpClient.get<Survey>(`${this.api}/consent/${id}`, this.httpOptions);
  }

  public updateSurvey(id?: string): Observable<Survey> {
    return this.httpClient.put<Survey>(`${this.api}/consent/${id}`, this.httpOptions);
  }

  public getAllQuestions(): Observable<Question[]> {
    return this.httpClient.get<Question[]>(`${this.api}/question`, this.httpOptions);
  }

  public getQuestionById(id?: string): Observable<Question> {
    return this.httpClient.get<Question>(`${this.api}/question/${id}`, this.httpOptions);
  }
  public getQuestionsBySurveyId(id?: string): Observable<SurveyQuestion[]> {
    var path = this.api + "/surveyQuestion/survey/" + id;
    //var path=`${this.api}​/surveyQuestion/survey​/${id}`;
    return this.httpClient.get<SurveyQuestion[]>(path, this.httpOptions);
  }
  public deleteBySurveyId(id?: string): Observable<SurveyQuestion> {
    var path = this.api + "/surveyQuestion/survey/" + id;
    return this.httpClient.delete<SurveyQuestion>(path, this.httpOptions);
  }
  public deleteSurvey(id?: string): Observable<Survey> {
    return this.httpClient.delete<Survey>(`${this.api}/consent/${id}`, this.httpOptions);
  }

  public patchUserRole(id?: string, role?: string): Observable<User> {
    let data = [{ op: "replace", path: "/role", value: role }];
    return this.httpClient.patch<User>(`${this.api}/user/${id}`, data, this.httpOptions);
  }
}
