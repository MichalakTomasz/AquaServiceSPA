import { Component } from '@angular/core';
import { AquaCalcService } from './services/aquaCalcService/aqua-calc.service';
import { CommonStringsService } from './services/commonStrings/common-strings.service';
import { EmailService } from './services/email/email.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers:[
    AquaCalcService,
    CommonStringsService,
    EmailService,
    { provide: 'BASE_URL', useValue: 'https://localhost:44307/api/' },
    { provide: 'DIGITS_DOUBLE_PRECISION_PATTERN', 
      useValue: '^[0-9]{0,2}[,.]{1}[0-9]{1,2}$|^[0-9]{1,2}[,.]{1}[0-9]{0,2}$|^[0-9]{1,2}$' },
    { provide: 'LONG_DIGITS_PATTERN',
      useValue: '^[0-9]*[,.]{1}[0-9]+$|^[0-9]+[,.]{1}[0-9]*$|^[0-9]+$'},
    { provide: 'DIGITS_PATTERN', useValue: '^[0-9]+$'}]
})
export class AppComponent {
  title = 'app';
}
