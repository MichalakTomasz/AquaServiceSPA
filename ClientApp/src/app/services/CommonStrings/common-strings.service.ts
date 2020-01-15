import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CommonStringsService {

  aquaLiters = 'Pojemność akwarium w litrach netto (bez żwiru, kamieni, korzeni...)';
  containerCapacity = 'Pojęmność pojemnika, w którym będzie sporządzany nawóz w mililitrach';
}
