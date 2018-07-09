import { HttpClient, HttpHeaders } from '@angular/common/http';

export class Headers {

  public GetPostHeaders(): HttpHeaders {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('jwt_token');
    headers.append('Authorization', `Bearer ${authToken}`);
    return headers;
  }
  public GetUnauthHeaders(): HttpHeaders {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    return headers;
  }
}
