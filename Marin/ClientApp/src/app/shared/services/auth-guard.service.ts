import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Response } from "@angular/http";
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  constructor(public router: Router, private httpClient: HttpClient) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    var token = sessionStorage.getItem("jwt_token");

    if (token) {
      return true;
    }
    this.router.navigate(['login']);
    return false;

  }

}
