import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CommonStringsService {

  aquaLiters = 'Pojemność akwarium w litrach netto (bez żwiru, kamieni, korzeni ect.)';
  containerCapacity = 'Pojęmność pojemnika, w którym będzie sporządzany nawóz w mililitrach';
  saltGToolTip = 'Ilość soli jaką chcesz wsypać do pojemnika';
}
