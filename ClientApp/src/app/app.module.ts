import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { Co2Component } from './components/co2/co2.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { MacroComponent } from './components/macro/macro.component';
import { AquaCalcService } from './services/AquaCalcService/aqua-calc.service';
import { ExpressComponent } from './components/express/express.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    Co2Component,
    PageNotFoundComponent,
    MacroComponent,
    ExpressComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'co2', component: Co2Component },
      { path: 'macro', component: MacroComponent },
      { path: 'express', component: ExpressComponent },
      { path: '**', component: PageNotFoundComponent }
    ]),
  ],
  providers: [
    {provide: 'BASE_URL', useValue: 'https://localhost:44307/api/'},
    {provide: 'DIGITS_DOUBLE_PRECISION_PATTERN', 
    useValue: '^[0-9]{0,2}[,.]{1}[0-9]{1,2}$|^[0-9]{1,2}[,.]{1}[0-9]{0,2}$|^[0-9]{1,2}$'},
    AquaCalcService],
  bootstrap: [AppComponent]
})
export class AppModule { }
