import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserService } from '../../shared/services/user.service';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute, Params } from '@angular/router';


@Component({
  selector: 'app-confirmemail',
  templateUrl: './confirmemail.component.html',
  styleUrls: ['./confirmemail.component.scss']
})
export class ConfirmemailComponent implements OnInit {
  private userEmail: string;
  private showLoader = true;
  private showError = false;
  private emailToken: string;
  private sub: Subscription;
  constructor(private route: ActivatedRoute,
    private router: Router, private userService: UserService) { }

  ngOnInit() {
    var router = this.router;
    this.sub = this.route.queryParams.subscribe((params: Params) => {
      this.userEmail = params['useremail'];
      this.emailToken = params['token'];
      this.userService.validateEmail(this.userEmail, this.emailToken).subscribe((value) => {
          this.showLoader = false;
        },
        error => {
          this.showLoader = false;
          this.showError = true;
        });
    });
    
    
  }

}
