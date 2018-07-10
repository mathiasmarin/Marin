import {HttpHeaders } from '@angular/common/http';

export class Headers {

  public GetPostHeadeOptions() {
    var token = sessionStorage.getItem("jwt_token");
    var httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      })
    };
    return httpOptions;
  }
  public GetUnauthHeadersOptions() {
    var httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'})
    };
    return httpOptions;
  }
}
