import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subscription, BehaviorSubject } from 'rxjs';
import { Headers } from '../classes/http-headers';
import { interval } from 'rxjs';
import { Router } from '@angular/router';
import { User } from '../classes/user';



@Injectable({
  providedIn: 'root'
})
export class UserService {
  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  _userName = new BehaviorSubject<string>('');
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();
  private loggedIn: boolean;
  httpOptions = this.headers.GetUnauthHeadersOptions();
  private interval = interval(1000 * 60);
  subscription: Subscription;


  constructor(private http: HttpClient, private headers: Headers, private router: Router) {
    this.loggedIn = !!sessionStorage.getItem('jwt_token');
    // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    this._authNavStatusSource.next(this.loggedIn);
  }

  login(userName: string, passWord: string, callback:Function) {
    var router = this.router;
      this.http.post("/api/Auth/Login",
      JSON.stringify({ userName: userName, password: passWord }),
      this.httpOptions).subscribe((value: any) => {
        if (value.hasOwnProperty("Token")) {
          sessionStorage.setItem("jwt_token", value.Token);
          this.loggedIn = true;
          this._authNavStatusSource.next(true);
          this._userName.next(value.FullName);
          this.validateToken(value.Token);
          callback();
          router.navigate(['']);
         
        }


      });
  }
  validateEmail(email: string, token: string): Observable<Object> {
    return this.http.post("/api/Auth/ConfirmEmail", JSON.stringify({ Email: email, Token: token }), this.httpOptions);
  }
  createNewUser(newUser: User): Observable<Object> {
    return this.http.post("/api/Auth/Register",
      JSON.stringify(newUser),
      this.httpOptions);
  }
  isLoggedIn() {
    return this.loggedIn;
  }
  logout() {
    sessionStorage.removeItem('jwt_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
    this.subscription.unsubscribe();
    this.router.navigate(['login']);
  }
  validateToken(token) {
    let http = this.http;
    let httpOptions = this.headers.GetPostHeadeOptions();
    this.subscription = this.interval.subscribe(successValue => {
      http.post("/api/Auth/IsUserLoggedIn", null, httpOptions).subscribe(x => {
        if (!successValue) {
          this.logout();
        }
      });
    });
  }
}
