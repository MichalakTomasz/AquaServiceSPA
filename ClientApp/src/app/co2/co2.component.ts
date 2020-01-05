import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICo2 } from 'src/interfaces/ico2';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-co2',
  templateUrl: './co2.component.html',
  styleUrls: ['./co2.component.css']
})
export class Co2Component implements OnInit {

  formGroup: FormGroup;
  co2Concentration: number;

  constructor(
    private http: HttpClient) { }

  ngOnInit() {
    this.formGroup = new FormGroup({
      ph: new FormControl(null, [
        Validators.required, 
        Validators.min(0),
        Validators.max(14),
        Validators.pattern('^[0-9]+$')]),
      kh: new FormControl(null, [
        Validators.required,
        Validators.min(0),
        Validators.max(30.8),
        Validators.pattern('^[0-9]+$')
      ])
    });
  }

  onSubmit() {
    let url = 'https://localhost:44307/api/co2';
    let co2: ICo2 = { 
      ph: +this.formGroup.value.ph, 
      kh: +this.formGroup.value.kh
    }

    this.http.post<number>(url, co2)
      .subscribe(result => {
        this.co2Concentration = result;
      },
      error => console.log(error));
  }
}
