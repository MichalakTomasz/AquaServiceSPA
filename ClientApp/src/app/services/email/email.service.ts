import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IEmail } from 'src/app/interfaces/i-email';
import { Observable } from 'rxjs';

@Injectable()
export class EmailService {

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private url) { }

  sendEmail(email: IEmail): Observable<boolean> {
    return this.http.post<boolean>(this.url + 'sendemail', email);
  }
}
