import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Answer } from '../models/answer';
import { Comments } from '../models/comment';
import { Historys } from '../models/history';
import { Notifications } from '../models/notification';
import { Question } from '../models/question';
import { Survey } from '../models/survey';
import { SurveyQuestion } from '../models/surveyQuestion';
import { User } from '../models/user';
import { UserDetail } from '../models/userDetail';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  // public isOpen=false;
  // public activeQuestions:Question[]=[];
  public currentIdUser!: string;
  private api: string = 'https://localhost:44311/api/v1';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  constructor(private readonly httpClient: HttpClient) {
  }

  public getUser(idUser: string): Observable<User> {
    return this.httpClient.get<User>(`${this.api}/user/${idUser}`, this.httpOptions);
  }
  public getUserDetail(idUser: string): Observable<UserDetail> {
    return this.httpClient.get<UserDetail>(`${this.api}/detail/user/${idUser}`, this.httpOptions);
  }

  public getAllSurveys(launchDate: string = "", expirationDate: string = "", legalBasis: string = ""): Observable<Survey[]> {
    // Initialize Params Object
    let params = new HttpParams();

    // Begin assigning parameters
    params = params.append('launchDateTime', launchDate);
    params = params.append('expirationDateTime', expirationDate);
    params = params.append('legalBasis', legalBasis);

    var httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      params: params ? params.keys().reduce((queryParams: any, key) => {

        let value: any = params.get(key);

        value = Array.isArray(value) ? value[0] : value;
        // adding only defined values to the params
        if (value !== "" && value !== null && value !== "null") {
          queryParams[key] = value;
        }
        return queryParams;
      }, {}) : {},
    };
    return this.httpClient.get<Survey[]>(`${this.api}/consent`, httpOptions);
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
  public CreateAnswer(answer: Answer): Observable<Answer> {
    return this.httpClient.post<Answer>(`${this.api}/answer`, answer, this.httpOptions);
  }
  public CreateComment(comment: Comments): Observable<Comments> {
    return this.httpClient.post<Comments>(`${this.api}/comment`, comment, this.httpOptions);
  }

  public UpdateAnswer(idAnswer: string, answer: Answer): Observable<Answer> {
    return this.httpClient.put<Answer>(`${this.api}/answer/${idAnswer}`, answer, this.httpOptions);
  }
  public UpdateComment(idComment: string, comment: Comments): Observable<Comments> {
    return this.httpClient.put<Comments>(`${this.api}/comment/${idComment}`, comment, this.httpOptions);
  }

  public GetAnswersByUserIdAndSurveyId(idUser: string, idSurvey: string): Observable<Answer[]> {
    var path = this.api + "/answer/user/" + idUser + "/survey/" + idSurvey;
    return this.httpClient.get<Answer[]>(path, this.httpOptions);
  }
  public GetCommentByUserIdAndSurveyId(idUser: string, idSurvey: string): Observable<Comments> {
    var path = this.api + "/comment/user/" + idUser + "/survey/" + idSurvey;
    return this.httpClient.get<Comments>(path, this.httpOptions);
  }

  public GetAllNotificationOfUser(idUser?: string): Observable<Notifications[]> {
    return this.httpClient.get<Notifications[]>(`${this.api}/notification/user/${idUser}/seen/false`, this.httpOptions);
  }

  public ReadNotification(idNotification: string, notification: Notifications): Observable<Notifications> {
    return this.httpClient.put<Notifications>(`${this.api}/notification/${idNotification}`, notification, this.httpOptions);
  }
  public CreateHistory(history: Historys): Observable<Historys> {
    return this.httpClient.post<Historys>(`${this.api}/history`, history, this.httpOptions);
  }
  public getAllHistorys(): Observable<Historys[]> {
    return this.httpClient.get<Historys[]>(`${this.api}/history`, this.httpOptions);
  }
}
