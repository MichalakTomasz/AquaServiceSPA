import { Component, OnInit } from '@angular/core';
import { IEmail } from 'src/app/interfaces/i-email';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  private model = <IEmail>{};

  constructor() { }

  ngOnInit() {
  }

}
