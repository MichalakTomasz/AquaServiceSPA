import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { Co2Component } from './co2/co2.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { MacroComponent } from './macro/macro.component';
import { JsonConverterService } from './services/json-converter.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    Co2Component,
    PageNotFoundComponent,
    MacroComponent
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
      { path: '**', component: PageNotFoundComponent }
    ]),
  ],
  providers: [JsonConverterService],
  bootstrap: [AppComponent]
})
export class AppModule { }
