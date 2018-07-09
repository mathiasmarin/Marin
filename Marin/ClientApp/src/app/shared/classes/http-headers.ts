import { HttpClient, HttpHeaders } from '@angular/common/http';

export class Headers {

  public GetPostHeaders(): HttpHeaders {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    let authToken = sessionStorage.getItem('auth_token');
    headers.append('Authorization', `Bearer ${authToken}`);
    return headers;
  }
  public GetUnauthHeaders(): HttpHeaders {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    return headers;
  }
}
