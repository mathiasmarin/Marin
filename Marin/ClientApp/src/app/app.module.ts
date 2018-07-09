import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { IndexComponent } from './index/index.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { InfoComponent } from './info/info.component';
import { Headers } from './shared/classes/http-headers';
import { UserService } from './shared/services/user.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    LoginComponent,
    IndexComponent,
    InfoComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [Headers, UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
