import { Component, OnInit, Inject } from '@angular/core';
import { AquaCalcService } from '../../services/aquaCalcService/aqua-calc.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { IK2so4 } from '../../interfaces/i-k2so4';
import { IK2so4Result } from '../../interfaces/i-k2so4-result';
import { CommonStringsService } from '../../services/commonStrings/common-strings.service';

@Component({
  selector: 'app-k2so4',
  templateUrl: './k2so4.component.html',
  styleUrls: ['./k2so4.component.css']
})
export class K2so4Component implements OnInit {

  public formGroup: FormGroup;
  public k2so4Result = <IK2so4Result>{};

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
      k2so4g: new FormControl('', [
        Validators.required, 
        Validators.min(0),
        Validators.pattern(this.longDigitsPattern)]),
    });
  }

  onSubmit(){
    let k2so4: IK2so4 = {
      aquaLiters: +this.formGroup.value.aquaLiters,
      containerCapacity: +this.formGroup.value.containerCapacity,
      k2so4g: +this.formGroup.value.k2so4g
    };

    this.aquaCalcService.computeK2So4(k2so4)
    .subscribe(result => {
      this.k2so4Result.potassiumContent = result.potassiumContent;
      this.k2so4Result.solubility = result.solubility;
      this.k2so4Result.solubilityInAmountWater = result.solubilityInAmountWater;
    }),
    error => console.log(error);
  }

  onInput() {
    this.k2so4Result.solubility = null;
    this.k2so4Result.potassiumContent = null;
  }
}
