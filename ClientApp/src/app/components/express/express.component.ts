import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { IExpressResult } from 'src/app/interfaces/i-express-result';
import { IExpress } from 'src/app/interfaces/i-express';
import { AquaCalcService } from '../../services/aquaCalcService/aqua-calc.service';
import { CommonStringsService } from 'src/app/services/commonStrings/common-strings.service';

@Component({
  selector: 'app-express',
  templateUrl: './express.component.html',
  styleUrls: ['./express.component.css']
})
export class ExpressComponent implements OnInit {

  public formGroup: FormGroup;
  public expressResult = <IExpressResult>{};
  public expressInfo = 'Kalkulator do sporządzania nawozów każdego w osobnym pojemniku. Po podaniu dwóch parametrów: wielkości akwarium i pojemności pojemnika na nawóz (ml), kalkulator uwzględniając zalecane proporcje nawozów, obliczy ile gram danej soli należy wsypać do danego pojemnika z wodą demineralizowaną. Wynikiem będzie ile miligramów na litr danego składnika będzie zawierał jeden mililitr naszego nawozu.';

  constructor(
    private aquaCalcService: AquaCalcService,
    private commonStringsService: CommonStringsService,
    @Inject('LONG_DIGITS_PATTERN')private longDigitsPattern) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      aquaLiters: new FormControl('', [
        Validators.required, 
        Validators.min(0),
        Validators.pattern(this.longDigitsPattern)]),
      containerCapacity: new FormControl('', 
      [
        Validators.required, 
        Validators.min(0),
        Validators.pattern(this.longDigitsPattern)])
    });
  }

  onSubmit(){
    let express: IExpress = {
      aquaLiters: +this.formGroup.value.aquaLiters,
      containerCapacity: +this.formGroup.value.containerCapacity
    };

    this.aquaCalcService.computeExpress(express)
    .subscribe(result => {
      this.expressResult.maxKNO3g = result.maxKNO3g;
      this.expressResult.maxConcentrationNinKNO3MgPerLiter 
        = result.maxConcentrationNinKNO3MgPerLiter;
      this.expressResult.maxConcentrationKinKNO3MgPerLiter 
        = result.maxConcentrationKinKNO3MgPerLiter;
      this.expressResult.optimalKNO3g = result.optimalKNO3g;
      this.expressResult.optimalConcentrationNinKNO3MgPerLiter 
        = result.optimalConcentrationNinKNO3MgPerLiter;
      this.expressResult.optimalConcentrationKinKNO3MgPerLiter
        = result.optimalConcentrationKinKNO3MgPerLiter;

      this.expressResult.maxKH2PO4g = result.maxKH2PO4g;
      this.expressResult.maxConcentrationPinKH2PO4MgPerLiter 
        = result.maxConcentrationPinKH2PO4MgPerLiter;
      this.expressResult.maxConcentrationKinKH2PO4MgPerLiter 
        = result.maxConcentrationKinKH2PO4MgPerLiter;
      this.expressResult.optimalKH2PO4g = result.optimalKH2PO4g;
      this.expressResult.optimalConcentrationPinKH2PO4MgPerLiter 
        = result.optimalConcentrationPinKH2PO4MgPerLiter;
      this.expressResult.optimalConcentrationKinKH2PO4MgPerLiter
        = result.optimalConcentrationKinKH2PO4MgPerLiter;

      this.expressResult.maxK2SO4g = result.maxK2SO4g;
      this.expressResult.maxConcentrationKinK2SO4MgPerLiter 
        = result.maxConcentrationKinK2SO4MgPerLiter;
      this.expressResult.optimalK2SO4g = result.optimalK2SO4g;
      this.expressResult.optimalConcentrationKinK2SO4MgPerLiter
        = result.optimalConcentrationKinK2SO4MgPerLiter;
      
      this.expressResult.maxMgSO4g = result.maxMgSO4g;
      this.expressResult.maxConcentrationMginMgSO4MgPerLiter
        = result.maxConcentrationMginMgSO4MgPerLiter;
      this.expressResult.optimalMgSO4g = result.optimalMgSO4g;
      this.expressResult.optimalConcentrationMginMgSO4MgPerLiter
        = result.optimalConcentrationMginMgSO4MgPerLiter;

    }),
    error => console.log(error);  
  }

  onInput() {
    this.expressResult.maxKNO3g = null;
  }
}
