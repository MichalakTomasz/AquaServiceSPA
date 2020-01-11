import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'round'
})
export class RoundPipe implements PipeTransform {

  transform(value: number, precision: number): string {
    let result = value.toFixed(precision);
    return result;
  }

}
