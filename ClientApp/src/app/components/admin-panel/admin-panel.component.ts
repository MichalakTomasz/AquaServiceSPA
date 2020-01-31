import { Component, OnInit } from '@angular/core';
import { IEmailSettings } from 'src/app/interfaces/i-email-settings';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { EmailService } from 'src/app/services/email/email.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  public formGroup: FormGroup;

  constructor(private emailService: EmailService) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      username: new FormControl('', Validators.required),
      emailAddress: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
      smtp: new FormControl('', Validators.required),
      port: new FormControl('', Validators.required),
      enableSsl: new FormControl(),
      isHtmlMessage: new FormControl()
    });
  }

  onSubmit() {
    let emailSettings: IEmailSettings = {
      username: this.formGroup.value.username,
      emailAddress: this.formGroup.value.emailAddress,
      password: this.formGroup.value.password,
      smtp: this.formGroup.value.smtp,
      port: +this.formGroup.value.port,
      enableSsl: this.formGroup.value.enableSsl,
      isHtmlMessage: this.formGroup.value.isHtmlMessage
    };

    this.emailService.sendEmailSettings(emailSettings)
    .subscribe(result => { console.log('email settings sent') }), 
    error => console.log(error);
  }
}
