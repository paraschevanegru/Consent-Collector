import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { combineLatest, Observable } from 'rxjs';
import { User } from 'src/app/models/user';
import { UserDetail } from 'src/app/models/userDetail';
import { Historys } from 'src/app/models/history';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private api: string = 'https://localhost:44311/api/v1';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
      })
  };

  constructor(private httpClient: HttpClient) {
  }

  public createUser(user:User):Observable<User> {
    return this.httpClient.post<User>(`${this.api}/user`, user, this.httpOptions);
  }
  public createDetailUser(userDetail:UserDetail):Observable<UserDetail>{
    return this.httpClient.post<UserDetail>(`${this.api}/detail`, userDetail, this.httpOptions);
  }

  public deleteUser(user:User):Observable<void>{
    return this.httpClient.delete<void>(`${this.api}/user/${user.id}`,this.httpOptions);
  }

  public CreateHistory(history:Historys):Observable<Historys>{
    return this.httpClient.post<Historys>(`${this.api}/history`,history,this.httpOptions);
  }
  public getAllHistorys():Observable<Historys[]>{
    return this.httpClient.get<Historys[]>(`${this.api}/history`, this.httpOptions);
  }

}
