import { Component, OnInit, Inject } from '@angular/core';
import { AquaCalcService } from 'src/app/services/aquaCalcService/aqua-calc.service';
import { CommonStringsService } from 'src/app/services/commonStrings/common-strings.service';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { IMgso4Result } from 'src/app/interfaces/i-mgso4-result';
import { IMgso4 } from 'src/app/interfaces/i-mgso4';

@Component({
  selector: 'app-mgso4',
  templateUrl: './mgso4.component.html',
  styleUrls: ['./mgso4.component.css']
})
export class Mgso4Component implements OnInit {

  private formGroup: FormGroup;
  private mgso4Result = <IMgso4Result>{}

  constructor(
    @Inject('LONG_DIGITS_PATTERN') private longDigitsPattern: string,
    @Inject('BASE_URL') private url: string,
    private aquaCalcService: AquaCalcService,
    private commonStringsService: CommonStringsService) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      aquaLiters: new FormControl('', [
        Validators.required,
        Validators.min(0),
        Validators.pattern(this.longDigitsPattern)]),
      containerCapacity: new FormControl('', [
        Validators.required,
        Validators.min(0), 
        Validators.pattern(this.longDigitsPattern)]),
      mgso4g: new FormControl('', [
        Validators.required, 
        Validators.min(0),
        Validators.pattern(this.longDigitsPattern)])
    });
  }

  onSubmit() {
    let mgso4: IMgso4 = {
      aquaLiters: +this.formGroup.value.aquaLiters,
      containerCapacity: +this.formGroup.value.containerCapacity,
      mgso4g: +this.formGroup.value.mgso4g
    };

    this.aquaCalcService.computemgso4(mgso4)
    .subscribe(result => {
      this.mgso4Result.magnesiumContent = result.magnesiumContent;
      this.mgso4Result.solubility = result.solubility;
      this.mgso4Result.solubilityInAmountWater = result.solubilityInAmountWater;
    }),
    error => console.log(error);    
  }

  onInput() {
    this.mgso4Result.solubility = null;
    this.mgso4Result.magnesiumContent = null;
  }
}
