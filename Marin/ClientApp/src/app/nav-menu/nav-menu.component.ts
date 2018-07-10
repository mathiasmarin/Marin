import { Component, OnInit, OnDestroy, NgModule } from '@angular/core';
import { Subscription } from 'rxjs';
import {UserService} from '../shared/services/user.service'

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})

@NgModule({
  providers: [UserService],
})

export class NavMenuComponent implements OnInit, OnDestroy  {
  isLoggedIn: boolean;
  subscription: Subscription;
  userName:string;

  constructor(private userService: UserService) {
    

  }
  Logout() {
    this.userService.logout();
  }
  ngOnInit() {
    this.subscription = this.userService.authNavStatus$.subscribe(status => this.isLoggedIn = status);
    this.userService._userName.subscribe(x => this.userName = x);
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
