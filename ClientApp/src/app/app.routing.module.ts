import { Routes, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { HomeComponent } from "./components/home/home.component";
import { Co2Component } from "./components/co2/co2.component";
import { MacroComponent } from "./components/macro/macro.component";
import { ExpressComponent } from "./components/express/express.component";
import { Kno3Component } from "./components/kno3/kno3.component";
import { K2so4Component } from "./components/k2so4/k2so4.component";
import { Kh2po4Component } from "./components/kh2po4/kh2po4.component";
import { Mgso4Component } from "./components/mgso4/mgso4.component";
import { ContactComponent } from "./components/contact/contact.component";
import { MessageSentComponent } from "./components/message-sent/message-sent.component";
import { AdminPanelComponent } from "./components/admin-panel/admin-panel.component";
import { PageNotFoundComponent } from "./components/page-not-found/page-not-found.component";
import { GridTestComponent } from "./components/grid-test/grid-test.component";

const appRoutes: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'co2', component: Co2Component },
    { path: 'macro', component: MacroComponent },
    { path: 'express', component: ExpressComponent },
    { path: 'kno3', component: Kno3Component },
    { path: 'k2so4', component: K2so4Component },
    { path: 'kh2po4', component: Kh2po4Component },
    { path: 'mgso4', component: Mgso4Component },
    { path: 'contact', component: ContactComponent },
    { path: 'messagesent', component: MessageSentComponent },
    { path: 'kjplolo', component: AdminPanelComponent },
    { path: 'gridtest', component: GridTestComponent },
    { path: '**', component: PageNotFoundComponent }
]

@NgModule({
    imports: [RouterModule.forRoot(appRoutes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }