import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';
import { UserDetail } from 'src/app/models/userDetail';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private api: string = 'https://localhost:44311/api/v1';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
      })
  };
  constructor(private readonly httpClient:HttpClient) {
  }

  public getUser(user:User):Observable<User>{
    return this.httpClient.get<User>(`${this.api}/user/username/${user.username}/password/${user.password}`,this.httpOptions);
  }
  public getUserDetail(user:User):Observable<UserDetail>{
    return this.httpClient.get<UserDetail>(`${this.api}/detail/user/${user.id}`,this.httpOptions);
  }
}
