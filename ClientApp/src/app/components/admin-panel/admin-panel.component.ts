import { Component, OnInit } from '@angular/core';
import { IEmailSettings } from 'src/app/interfaces/i-email-settings';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { EmailService } from 'src/app/services/email/email.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  public formGroup: FormGroup;

  constructor(
    private emailService: EmailService,
    private router: Router) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      emailAddress: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      smtp: new FormControl('', Validators.required),
      port: new FormControl('', Validators.required),
      enableSsl: new FormControl(),
      isHtmlMessage: new FormControl()
    });
  }

  onSubmit() {
    let emailAddress: string = this.formGroup.value.emailAddress;
    let emailAddressLength = emailAddress.indexOf('@');
    let username = emailAddress.substring(0, emailAddressLength);
    let emailSettings: IEmailSettings = {
      username: username,
      emailAddress: this.formGroup.value.emailAddress,
      password: this.formGroup.value.password,
      smtp: this.formGroup.value.smtp,
      port: +this.formGroup.value.port,
      enableSsl: this.formGroup.value.enableSsl,
      isHtmlMessage: this.formGroup.value.isHtmlMessage
    };

    this.emailService.sendEmailSettings(emailSettings)
    .subscribe(result => { 
      console.log('email settings sent');
    }), 
    error => console.log(error);
  }
}
