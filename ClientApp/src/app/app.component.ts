import { Component } from '@angular/core';
import { AquaCalcService } from './services/aquaCalcService/aqua-calc.service';
import { CommonStringsService } from './services/commonStrings/common-strings.service';
import { EmailService } from './services/email/email.service';
import { VisitService } from './services/visit/visit.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers:[
    AquaCalcService,
    CommonStringsService,
    VisitService,
    EmailService,
    { provide: 'DIGITS_DOUBLE_PRECISION_PATTERN', 
      useValue: '^[0-9]{0,2}[,.]{1}[0-9]{1,2}$|^[0-9]{1,2}[,.]{1}[0-9]{0,2}$|^[0-9]{1,2}$' },
    { provide: 'LONG_DIGITS_PATTERN',
      useValue: '^[0-9]*[,.]{1}[0-9]+$|^[0-9]+[,.]{1}[0-9]*$|^[0-9]+$'},
    { provide: 'DIGITS_PATTERN', useValue: '^[0-9]+$'}]
})
export class AppComponent {

  constructor(private visitService: VisitService) {}

  title = 'app';

  ngOnInit(): void {
    this.visitService.saveVisit()
    .subscribe(result => console.log('saved IP:' + result))
  }
}
