import { Component, OnInit } from '@angular/core';
import { UserService } from '../../shared/services/user.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.scss']
})
export class ResetpasswordComponent implements OnInit {
  private userEmail: string;
  private showLoader = true;
  private showError = false;
  private emailToken: string;
  private newPassword: string;
  private sub: Subscription;
  private showSpinner = false;
  private showConfirm = false;
  constructor(private route: ActivatedRoute,
    private router: Router, private userService: UserService) { }

  ngOnInit() {
    this.sub = this.route.queryParams.subscribe((params: Params) => {
      this.userEmail = params['useremail'];
      this.emailToken = params['token'];
    });
  }
  resetPassword() {
    this.showSpinner = true;
    this.userService.setNewPassword(this.userEmail, this.emailToken, this.newPassword).subscribe((value) => {
      this.showSpinner = false;
      this.showConfirm = true;
    },
      error => {
        this.showSpinner = false;
      });
  }

}
