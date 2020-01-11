import { Component, OnInit, Inject } from '@angular/core';
import { AquaCalcService } from 'src/app/services/AquaCalcService/aqua-calc.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IKno3 } from 'src/app/interfaces/i-kno3';
import { IKno3Result } from 'src/app/interfaces/i-kno3-result';

@Component({
  selector: 'app-kno3',
  templateUrl: './kno3.component.html',
  styleUrls: ['./kno3.component.css']
})
export class Kno3Component implements OnInit {

  private formGroup: FormGroup;
  private kno3Result = <IKno3Result>{};

  constructor(
    @Inject('DIGITS_PATTERN') private digitsPattern: string,
    @Inject('BASE_URL') private url: string,
    private aquaCalcService: AquaCalcService) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      aquaLiters: new FormControl('', [
        Validators.required, 
        Validators.min(0),
        Validators.pattern(this.digitsPattern)]),
      containerCapacity: new FormControl('', [
        Validators.required,
        Validators.min(0), 
        Validators.pattern(this.digitsPattern)]),
      kno3g: new FormControl('', [
        Validators.required, 
        Validators.min(0),
        Validators.pattern(this.digitsPattern)]),
    });
  }

  onSubmit() {
    let kno3 : IKno3 = {
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
}
