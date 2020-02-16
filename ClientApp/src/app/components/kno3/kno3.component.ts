import { Component, OnInit, Inject } from '@angular/core';
import { AquaCalcService } from '../../services/aquaCalcService/aqua-calc.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IKno3 } from '../../interfaces/i-kno3';
import { IKno3Result } from '../../interfaces/i-kno3-result';
import { CommonStringsService } from '../../services/commonStrings/common-strings.service';

@Component({
  selector: 'app-kno3',
  templateUrl: './kno3.component.html',
  styleUrls: ['./kno3.component.css']
})
export class Kno3Component implements OnInit {

  public formGroup: FormGroup;
  public kno3Result = <IKno3Result>{};

  constructor(
    @Inject('LONG_DIGITS_PATTERN') private longDigitsPattern: string,
    @Inject('BASE_URL') private url: string,
    private aquaCalcService: AquaCalcService,
    public commonStringsService: CommonStringsService) { }

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
      kno3g: new FormControl('', [
        Validators.required, 
        Validators.min(0),
        Validators.pattern(this.longDigitsPattern)])
    });
  }

  onSubmit() {
    let kno3: IKno3 = {
      aquaLiters: +this.formGroup.value.aquaLiters,
      containerCapacity: +this.formGroup.value.containerCapacity,
      kno3g: +this.formGroup.value.kno3g
    };

    this.aquaCalcService.computeKno3(kno3)
    .subscribe(result => {
      this.kno3Result.potassiumContent = result.potassiumContent;
      this.kno3Result.nitrogenContent = result.nitrogenContent;
      this.kno3Result.solubility = result.solubility;
      this.kno3Result.solubilityInAmountWater = result.solubilityInAmountWater;
    }),
    error => console.log(error);
  }

  onInput() {
    this.kno3Result.nitrogenContent = null;
    this.kno3Result.solubility = null;
  }
}
