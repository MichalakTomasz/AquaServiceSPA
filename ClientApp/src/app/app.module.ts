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
import { ExpressComponent } from './components/express/express.component';
import { Kno3Component } from './components/kno3/kno3.component';
import { RoundPipe } from './pipes/round.pipe';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    Co2Component,
    PageNotFoundComponent,
    MacroComponent,
    ExpressComponent,
    Kno3Component,
    RoundPipe
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
      { path: 'kno3', component: Kno3Component },
      { path: '**', component: PageNotFoundComponent }
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
