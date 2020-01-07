import { Component } from '@angular/core';
import { AquaCalcService } from './services/AquaCalcService/aqua-calc.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers:[
    AquaCalcService,
    { provide: 'BASE_URL', useValue: 'https://localhost:44307/api/' },
    { provide: 'DIGITS_DOUBLE_PRECISION_PATTERN', 
      useValue: '^[0-9]{0,2}[,.]{1}[0-9]{1,2}$|^[0-9]{1,2}[,.]{1}[0-9]{0,2}$|^[0-9]{1,2}$' }]
})
export class AppComponent {
  title = 'app';
}
