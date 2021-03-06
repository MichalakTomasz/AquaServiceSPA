import { Component, OnInit, Inject } from '@angular/core';
import { ICo2 } from '../../interfaces/i-co2';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AquaCalcService } from '../../services/aquaCalcService/aqua-calc.service';

@Component({
  selector: 'app-co2',
  templateUrl: './co2.component.html',
  styleUrls: ['./co2.component.css']
})
export class Co2Component implements OnInit {

  public formGroup: FormGroup;
  public co2Concentration: number;
  public phToolTip: string;
  public khToolTip: string;

  constructor(
    private aquaCalcService: AquaCalcService,
    @Inject('DIGITS_DOUBLE_PRECISION_PATTERN') private digitsDoublePrecisionPattern) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      ph: new FormControl('', [
        Validators.required, 
        Validators.min(0),
        Validators.max(14),
        Validators.pattern(this.digitsDoublePrecisionPattern)]),
      kh: new FormControl('', [
        Validators.required,
        Validators.min(0),
        Validators.max(30.8),
        Validators.pattern(this.digitsDoublePrecisionPattern)
      ])
    });

    this.phToolTip = 'Podaj wartość pH';
    this.khToolTip = "Podaj wartość kH";
  }

  onSubmit() {
    let co2: ICo2 = { 
      ph: +this.formGroup.value.ph.toString().replace(',', '.'), 
      kh: +this.formGroup.value.kh
    }

     this.aquaCalcService.computeCo2(co2)
     .subscribe(result => this.co2Concentration = result);
  }

  onInput() {
    this.co2Concentration = null;
  }
}
