import { Component } from '@angular/core';
import { Router, Event, NavigationCancel } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';
  constructor(private router: Router) {

    router.events.subscribe((event: Event) => {

      if (event instanceof NavigationCancel) {
        //This is due to route guard returning false. Redirect to login
        this.router.navigate(['login']);
      }
    });

  }
}
