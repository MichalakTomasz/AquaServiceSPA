import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { IExpressResult } from 'src/app/interfaces/i-express-result';
import { IExpress } from 'src/app/interfaces/i-express';
import { AquaCalcService } from '../../services/AquaCalcService/aqua-calc.service';

@Component({
  selector: 'app-express',
  templateUrl: './express.component.html',
  styleUrls: ['./express.component.css']
})
export class ExpressComponent implements OnInit {

  private formGroup: FormGroup;
  private expressResult = <IExpressResult>{}; 

  constructor(private aquaCalcService: AquaCalcService) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      aquaLiters: new FormControl('', [Validators.required, Validators.min(0)]),
      containerCapacity: new FormControl('', [Validators.required, Validators.min(0)])
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
}
