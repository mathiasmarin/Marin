import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subscription, BehaviorSubject } from 'rxjs';
import { Headers } from '../classes/http-headers';
import { interval } from 'rxjs';
import { Router } from '@angular/router';



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
  private interval = interval(1000 * 10);
  subscription: Subscription;


  constructor(private http: HttpClient, private headers: Headers, private router: Router) {
    this.loggedIn = !!sessionStorage.getItem('jwt_token');
    // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    this._authNavStatusSource.next(this.loggedIn);
  }

  async Login(userName: string, passWord: string, callback:Function) {
    var router = this.router;
    return this.http.post("/api/Auth/Login",
      JSON.stringify({ userName: userName, password: passWord }),
      this.httpOptions).subscribe((value: any) => {
        if (value.hasOwnProperty("Token")) {
          sessionStorage.setItem("jwt_token", value.Token);
          this.loggedIn = true;
          this._authNavStatusSource.next(true);
          this.validateToken(value.Token);
          router.navigate(['']);
          callback();
        }


      });
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
    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization':`Bearer ${token}`
      })
  };
    this.subscription = this.interval.subscribe(x => {
      http.post("/api/Auth/IsUserLoggedIn", null, httpOptions).subscribe(x => {
        if (!x) {
          this.logout();
        }
      });
    });
  }
}
