import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IEmail } from 'src/app/interfaces/i-email';
import { Observable } from 'rxjs';
import { IEmailSettings } from 'src/app/interfaces/i-email-settings';

@Injectable()
export class EmailService {

  private baseUrl: string;

  constructor(
    private http: HttpClient) { 
      this.baseUrl = location.origin + '/api/';
    }

  sendEmail(email: IEmail): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + 'sendcontactemail', email);
  }

  sendEmailSettings(sendEmailSettings: IEmailSettings): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + 'setemailsettings', sendEmailSettings);
  }
}
