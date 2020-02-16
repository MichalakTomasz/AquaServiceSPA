import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { IMacro } from '../../interfaces/i-macro';
import { IMacroResult } from '../../interfaces/i-macro-result';
import { IAquaMacroDefaultSettings } from '../../interfaces/i-aqua-macro-default-settings';
import { AquaCalcService } from '../../services/aquaCalcService/aqua-calc.service';
import { CommonStringsService } from '../../services/commonStrings/common-strings.service';

@Component({
  selector: 'app-macro',
  templateUrl: './macro.component.html',
  styleUrls: ['./macro.component.css']
})
export class MacroComponent implements OnInit {

  constructor(
    private aquaCalcService: AquaCalcService,
    public commonStringsService: CommonStringsService,
    @Inject('DIGITS_DOUBLE_PRECISION_PATTERN') private digitsDoublePrecisionPattern,
    @Inject('LONG_DIGITS_PATTERN') private longDigitsPattern,
    @Inject('DIGITS_PATTERN') private digitsPattern) {}

  public formGroup: FormGroup;
  public macroResult = <IMacroResult>{};
  public aquaMacroDefaultSettings = <IAquaMacroDefaultSettings>{}; 
  public timesAWeekToolTip = 'Ile razy w tygodniu chcesz podawać nawóz';
  public nitrogenToolTip = 'Jakie stężenie azotu chcesz uzyskać po tygodniowym dawkowaniu nawozu';
  public phosphorusToolTip = 'Jakie stężenie fosforu chcesz uzyskać po tygodniowym dawkowaniu nawozu';
  public potassiumToolTip = 'Jakie stężenie potasu chcesz uzyskać po tygodniowym dawkowaniu nawozu';
  public magnesiumToolTip = 'Jakie stężenie magnezu chcesz uzyskać po tygodniowym dawkowaniu nawozu'; 
  public macroInfo = '"Kalkulator Makro" służy do sporządzenia nawozu z wymaganych soli mineralnych w jednym pojemniku. W miejscu "Parametry do sporządzenia nawozu" podajemy: ile litrów wody (bez podłoża, kamieni, korzeni, roślin, ect.) mamy w akwarium. Następnie podajemy w jakiej ilości wody będziemy mieszać nawóz oraz ile razy w tygodniu chcemy podawać nawóz. Następnie podajemy jakie stężenia pierwiastków (azotu, fosforu, potasu, magnezu) chcemy uzyskać po tygodniowym dawkowaniu nawozu. Jeżeli mamy zamiar zrobić nawóz bez jakiegoś składnika w jego miejscu wpisujemy 0. Kalkulator oblicza ile gram każdej soli musimy dodać do ustalonej wcześniej ilości wody demineralizowanej i ilość mililitrów jednorazowej dawki nawozu.';

  ngOnInit(){
    this.getMacroDefaultSettings();
    this.formGroup = new FormGroup({
      aquaLiters: new FormControl('',
        [Validators.required, Validators.min(0), Validators.pattern(this.longDigitsPattern)]),
      containerCapacity: new FormControl('',
        [Validators.required, Validators.min(0), Validators.pattern(this.longDigitsPattern)]),
      timesAWeek: new FormControl('',
        [Validators.required, Validators.min(1), Validators.max(7), Validators.pattern(this.digitsPattern)]),
      nitrogen: new FormControl('',
        [Validators.required, Validators.min(0), Validators.max(80), Validators.pattern(this.digitsPattern)]),
      phosphorus: new FormControl('',
        [Validators.required, Validators.min(0), Validators.max(10), Validators.pattern(this.digitsDoublePrecisionPattern)]),
      potassium: new FormControl('',
        [Validators.required, Validators.min(0), Validators.max(80), Validators.pattern(this.digitsPattern)]),
      magnesium: new FormControl('',
        [Validators.required, Validators.min(0), Validators.max(100), Validators.pattern(this.digitsPattern)])
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
      phosphorus: +this.formGroup.value.phosphorus.toString().replace(',', '.'),
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
