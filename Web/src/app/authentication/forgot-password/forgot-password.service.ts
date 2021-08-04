import { useAnimation } from '@angular/animations';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';
import { UserDetail } from 'src/app/models/userDetail';

@Injectable({
  providedIn: 'root'
})
export class ForgotPasswordService {

  private api: string = 'https://localhost:44311/api/v1';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
      })
  };
  constructor(private readonly httpClient:HttpClient) {
  }

  public getUser(userId:string):Observable<User>
  {
    return this.httpClient.get<User>(`${this.api}/user/${userId}`,this.httpOptions);
  }

  public postNewPassword(idUser:string, user: User):Observable<User>
  {
    return this.httpClient.put<User>(`${this.api}/user/${idUser}`, user, this.httpOptions);
  }

  public getUserDetail(email:string, number:string):Observable<UserDetail>
  {
    return this.httpClient.get<UserDetail>(`${this.api}/detail/email/${email}/number/${number}`,this.httpOptions);
  }
}
