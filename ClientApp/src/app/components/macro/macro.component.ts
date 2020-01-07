import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validator, Validators } from '@angular/forms';
import { IMacro } from 'src/app/interfaces/i-macro';
import { IMacroResult } from 'src/app/interfaces/i-macro-result';
import { IAquaMacroDefaultSettings } from 'src/app/interfaces/i-aqua-macro-default-settings';
import { AquaCalcService } from '../../services/AquaCalcService/aqua-calc.service';

@Component({
  selector: 'app-macro',
  templateUrl: './macro.component.html',
  styleUrls: ['./macro.component.css']
})
export class MacroComponent implements OnInit {

  constructor(
    private aquaCalcService: AquaCalcService,
    @Inject('DIGITS_DOUBLE_PRECISION_PATTERN') private digitsDoublePrecisionPattern) {}

  private formGroup: FormGroup;
  private macroResult = <IMacroResult>{};
  private aquaMacroDefaultSettings = <IAquaMacroDefaultSettings>{};  
 
  ngOnInit(){
    this.getMacroDefaultSettings();
    this.formGroup = new FormGroup({
      aquaLiters: new FormControl('',
        [Validators.required, Validators.min(0), Validators.pattern('^[0-9]+$')]),
      containerCapacity: new FormControl('',
        [Validators.required, Validators.min(0), Validators.pattern('^[0-9]+$')]),
      timesAWeek: new FormControl('',
        [Validators.required, Validators.min(1), Validators.max(7), Validators.pattern('^[0-9]+$')]),
      nitrogen: new FormControl('',
        [Validators.required, Validators.min(0), Validators.max(80), Validators.pattern('^[0-9]+$')]),
      phosphorus: new FormControl('',
        [Validators.required, Validators.min(0), Validators.max(10), Validators.pattern(this.digitsDoublePrecisionPattern)]),
      potassium: new FormControl('',
        [Validators.required, Validators.min(0), Validators.max(80), Validators.pattern('^[0-9]+$')]),
      magnesium: new FormControl('',
        [Validators.required, Validators.min(0), Validators.max(100), Validators.pattern('^[0-9]+$')])
    });
  }

  getMacroDefaultSettings(){
    this.aquaCalcService.getMacroDafautSettings()
    .subscribe(result => {

      this.aquaMacroDefaultSettings = result;
      this.formGroup.get('aquaLiters').setValue(this.aquaMacroDefaultSettings.aquaLiters);
      this.formGroup.get('containerCapacity').setValue(this.aquaMacroDefaultSettings.containerCapacity);
      this.formGroup.get('timesAWeek').setValue(this.aquaMacroDefaultSettings.timesAWeek);
      this.formGroup.get('nitrogen').setValue(this.aquaMacroDefaultSettings.nitrogen);
      this.formGroup.get('phosphorus').setValue(this.aquaMacroDefaultSettings.phosphorus);
      this.formGroup.get('potassium').setValue(this.aquaMacroDefaultSettings.potassium);
      this.formGroup.get('magnesium').setValue(this.aquaMacroDefaultSettings.magnesium);
    }),
      error => console.log(error);
  }

  onSubmit(){
    let macro: IMacro = {
      aquaLiters: +this.formGroup.value.aquaLiters,
      containerCapacity: +this.formGroup.value.containerCapacity,
      timesAWeek: +this.formGroup.value.timesAWeek,
      nitrogen: +this.formGroup.value.nitrogen,
      phosphorus: +(this.formGroup.value.phosphorus as string).replace(',', '.'),
      potassium: +this.formGroup.value.potassium,
      magnesium: +this.formGroup.value.magnesium
    };

    this.aquaCalcService.computeMacro(macro)
    .subscribe(result => {
      this.macroResult.kno3 = result.kno3,
      this.macroResult.k2so4 = result.k2so4,
      this.macroResult.kh2po4 = result.kh2po4,
      this.macroResult.mgso4 = result.mgso4,
      this.macroResult.singleDose = result.singleDose
    },
    error => console.log(error));
  }
}
