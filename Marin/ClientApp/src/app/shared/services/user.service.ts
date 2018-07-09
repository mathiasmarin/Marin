import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { Headers } from '../classes/http-headers';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();
  private loggedIn: boolean;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient, private headers: Headers) {
    this.loggedIn = !!sessionStorage.getItem('jwt_token');
    // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    this._authNavStatusSource.next(this.loggedIn);
  }

  Login(userName, passWord) {
    var test = "test";
    return this.http.post("/api/Auth/Login",
      JSON.stringify({ userName: userName, password: passWord }),this.httpOptions).toPromise().then((value: any) => {
      if (value.hasOwnProperty("Token")) {
        sessionStorage.setItem("jwt_token", value.Token);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
      }


    }).catch(reason => {
      console.log(reason);
    });
  }
  isLoggedIn() {
    return this.loggedIn;
  }
  logout() {
    sessionStorage.removeItem('jwt_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
  }
}
