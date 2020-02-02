import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class VisitService {

  constructor(private http: HttpClient) { }

  saveVisit(): Observable<string> {
    return this.http.get<string>(location.origin + '/api/savevisit');
  }
}
