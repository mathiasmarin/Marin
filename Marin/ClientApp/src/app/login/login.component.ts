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
  userName: string;
  password: string;
  showNewUser:false;
  newuser = new User();

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  showSpinner = false;
  constructor(private router: Router, private http: HttpClient, private userService: UserService) {
  }

  ngOnInit() {
  }

  login() {
    this.showSpinner = true;
    this.userService.login(this.userName, this.password, () => {
      this.showSpinner = false;
    });
  }
  CreateNewUser() {

  }
}
