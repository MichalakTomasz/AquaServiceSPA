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
import { ToolTipDirective } from './directives/tooltip/tool-tip.directive';
import { ClickInfoDirective } from './directives/clickInfo/click-info.directive';
import { PopupInfoDirective } from './directives/popupInfo/popup-info.directive';
import { K2so4Component } from './components/k2so4/k2so4.component';
import { Kh2po4Component } from './components/kh2po4/kh2po4.component';
import { Mgso4Component } from './components/mgso4/mgso4.component';
import { ContactComponent } from './components/contact/contact.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { MessageSentComponent } from './components/message-sent/message-sent.component';
import { FooterComponent } from './components/footer/footer.component';
import { AppRoutingModule } from './app.routing.module';
import { GridTestComponent } from './components/grid-test/grid-test.component';
import { DataGridComponent } from './modules/data-grid/data-grid-component/data-grid.component';
import { DataGridModule } from './modules/data-grid/data-grid.module';
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
    RoundPipe,
    ToolTipDirective,
    ClickInfoDirective,
    PopupInfoDirective,
    K2so4Component,
    Kh2po4Component,
    Mgso4Component,
    ContactComponent,
    AdminPanelComponent,
    MessageSentComponent,
    FooterComponent,
    GridTestComponent,
    DataGridComponent,
    DataGridModule
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
