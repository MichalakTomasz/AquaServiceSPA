import { Component, OnInit } from '@angular/core';
import { IEmail } from 'src/app/interfaces/i-email';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { EmailService } from 'src/app/services/email/email.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  public formGroup: FormGroup;

  constructor(private emailService: EmailService) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      emailAddress: new FormControl('', Validators.required),
      subject: new FormControl('', Validators.required),
      message: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required)
    });
  }

  onSubmit() {
    let email: IEmail = {
      username: this.formGroup.value.username,
      emailAddress: this.formGroup.value.emailAddress,
      subject: this.formGroup.value.subject,
      message: this.formGroup.value.message,
      description: this.formGroup.value.description
    }

    this.emailService.sendEmail(email)
    .subscribe(result => console.log(result)),
    error => console.log(error);
  }
}
