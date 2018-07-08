import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Response } from "@angular/http";
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { User } from '../user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {
  UserName: string;
  Password: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  constructor(private router: Router, private http: HttpClient) {
  }

  ngOnInit() {
  }

  Login() {
    console.log(this.UserName);
    this.http.post("/api/Auth/Login", JSON.stringify({ userName: this.UserName, password: this.Password}),this.httpOptions).toPromise().then(function(value) {
      this.router.navigate(['']);

    }).catch(function(reason) {

    })
  }
}
