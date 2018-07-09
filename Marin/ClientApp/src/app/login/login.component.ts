import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Response } from "@angular/http";
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { User } from '../shared/classes/user';
import { UserService } from '../shared/services/user.service';


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
  constructor(private router: Router, private http: HttpClient, private userService: UserService) {
  }

  ngOnInit() {
  }

  Login() {
    const router = this.router;
    this.userService.Login(this.UserName, this.Password);
  }
}
