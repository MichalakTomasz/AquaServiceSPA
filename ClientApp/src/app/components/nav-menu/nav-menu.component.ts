import { Component, Renderer, Renderer2 } from '@angular/core';
import { IMenuItem } from '../../interfaces/i-menu-item';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  mainMenuItems: Array<IMenuItem> = [
    { name:'Główna', link:'/'}, 
    { name:'Kalkulatory', link:''}, 
    { name:'Kontakt', link: 'contact'}];
  calcMenuItems: Array<IMenuItem> = [
    { name: 'Ekspresowy', link: 'express'}, 
    { name: 'Makro', link: 'macro'},
    { name: 'CO2', link: 'co2'}, 
    { name: 'KNO3', link: 'kno3'}, 
    { name:'KH2PO4', link: 'kh2po4'},
    { name: 'K2SO4', link: 'k2so4'},
    { name: 'MgSO4*7H2O', link: 'mgso4'}];
  loggedMenuItems = ['Profil', 'Wyloguj'];
  loginMenuItems = ['Zaloguj', 'Zarejestruj'];  
}