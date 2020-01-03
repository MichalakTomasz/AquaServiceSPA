import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl } from '@angular/forms';
import { IMacro } from 'src/interfaces/imacro';
import { IMacroResult } from 'src/interfaces/imacro-result';

@Component({
  selector: 'app-macro',
  templateUrl: './macro.component.html',
  styleUrls: ['./macro.component.css']
})
export class MacroComponent implements OnInit {

  private formGroup: FormGroup;
  private macroResult: IMacroResult = <IMacroResult>{};

  constructor(
    private http: HttpClient) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      aquaLiters: new FormControl(''),
      containerCapacity: new FormControl(''),
      timesAWeek: new FormControl(''),
      nitrogen: new FormControl(''),
      phosphorus: new FormControl(''),
      potassium: new FormControl(''),
      magnesium: new FormControl('')
    });
  }

  onSubmit(){
    let macro: IMacro = {
      aquaLiters: +this.formGroup.value.aquaLiters,
      containerCapacity: +this.formGroup.value.containerCapacity,
      timesAWeek: +this.formGroup.value.timesAWeek,
      nitrogen: +this.formGroup.value.nitrogen,
      phosphorus: +this.formGroup.value.phosphorus,
      potassium: +this.formGroup.value.potassium,
      magnesium: +this.formGroup.value.magnesium
    }

    let url = 'https://localhost:44307/api/macro';
    this.http.post<IMacroResult>(url, macro)
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
