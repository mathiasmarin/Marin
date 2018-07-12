import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ConfirmemailComponent } from './login/confirmemail/confirmemail.component';
import { InfoComponent } from './info/info.component';
import { AuthGuardService as AuthGuard } from './shared/services/auth-guard.service';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'info', component: InfoComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'login/confirmemail', component: ConfirmemailComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
