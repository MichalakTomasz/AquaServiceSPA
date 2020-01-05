import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JsonConverterService {

  toJson(object: any): any{
    let stringJson = JSON.stringify(object);
    return JSON.parse(stringJson);
  }
}
