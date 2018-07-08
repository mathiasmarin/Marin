import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Response } from "@angular/http";
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardServiceService implements CanActivate {
  constructor(public router: Router, private httpClient: HttpClient) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    return this.httpClient.get<boolean>("/api/Auth/IsUserLoggedIn").toPromise();

  }

}
