import { Component, OnInit, Inject } from '@angular/core';
import { AquaCalcService } from 'src/app/services/aquaCalcService/aqua-calc.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { IKh2po4Result } from 'src/app/interfaces/i-kh2po4-result';
import { IKh2po4 } from 'src/app/interfaces/i-kh2po4';
import { CommonStringsService } from 'src/app/services/commonStrings/common-strings.service';

@Component({
  selector: 'app-kh2po4',
  templateUrl: './kh2po4.component.html',
  styleUrls: ['./kh2po4.component.css']
})
export class Kh2po4Component implements OnInit {

  private formGroup: FormGroup;
  private kh2po4Result = <IKh2po4Result>{};

  constructor(
    @Inject('LONG_DIGITS_PATTERN') private longDigitsPattern: string,
    @Inject('BASE_URL') private url: string,
    private aquaCalcService: AquaCalcService,
    private commonStringsService: CommonStringsService
  ) { }

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
      kh2po4g: new FormControl('', [
        Validators.required, 
        Validators.min(0),
        Validators.pattern(this.longDigitsPattern)])
    });
  }

  onSubmit() {
    let kh2po4: IKh2po4 = {
      aquaLiters: +this.formGroup.value.aquaLiters,
      containerCapacity: +this.formGroup.value.containerCapacity,
      kh2po4g: +this.formGroup.value.kh2po4g
    };

    this.aquaCalcService.computeKh2po4(kh2po4)
    .subscribe(result => {
      this.kh2po4Result.phosphorusContent = result.phosphorusContent;
      this.kh2po4Result.potassiumContent = result.potassiumContent;
      this.kh2po4Result.solubility = result.solubility;
      this.kh2po4Result.solubilityInAmountWater = result.solubilityInAmountWater;
    }),
    error => console.log(error);
  }

  onInput() {
    this.kh2po4Result.solubility = null;
    this.kh2po4Result.phosphorusContent = null;
  }
}
