import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICo2 } from 'src/app/interfaces/i-co2'

import { Observable } from 'rxjs';
import { IMacro } from '../../interfaces/i-macro';
import { IMacroResult } from '../../interfaces/i-macro-result';
import { IAquaMacroDefaultSettings } from '../../interfaces/i-aqua-macro-default-settings';
import { IExpress } from '../../interfaces/i-express';
import { IExpressResult } from 'src/app/interfaces/i-express-result';
import { IKno3 } from 'src/app/interfaces/i-kno3';
import { IKno3Result } from 'src/app/interfaces/i-kno3-result';

@Injectable()
export class AquaCalcService {

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private url: string) { }

  computeCo2(co2: ICo2): Observable<number>{
    return this.http.post<number>(this.url + 'co2', co2);
  }

  getMacroDafautSettings(): Observable<IAquaMacroDefaultSettings> {
    return this.http.get<IAquaMacroDefaultSettings>(this.url + 'macrodefaultdata')
  }

  computeMacro(macro: IMacro): Observable<IMacroResult>{
    return this.http.post<IMacroResult>(this.url + 'macro', macro);
  }

  computeExpress(express: IExpress): Observable<IExpressResult>{
    return this.http.post<IExpressResult>(this.url + 'express', express)
  }

  computeKno3(kno3: IKno3): Observable<IKno3Result>{
    return this.http.post<IKno3Result>(this.url + 'kno3', kno3);
  }
}
