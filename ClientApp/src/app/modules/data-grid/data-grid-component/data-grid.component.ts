import { Component, OnInit, Renderer2, Input, ElementRef, ViewChild } from '@angular/core';
import { IOptions } from '../interfaces/i-options';
@Component({
  selector: 'data-grid',
  templateUrl: './data-grid.component.html',
  styleUrls: ['./data-grid.component.css']
})
export class DataGridComponent implements OnInit {

  constructor(private renderer: Renderer2) { }

  @Input()
  dataSource: [];

  @Input()
  options = <IOptions>{};

  @ViewChild('tableRef', { static: true})
  tableElement: ElementRef;

  ngOnInit(){
    if (this.dataSource && this.options?.columns) {
        let headerRowElem = this.renderer.createElement('tr');
        this.options.columns.forEach((column, index) => {
          if (column.isVisible){
            let columnElem = this.renderer.createElement('th');
            if (column.displayName){
              columnElem.innerText = columnElem.displayName;
            }
            else if (column.name){
              columnElem.innerText = column.name
            }
            this.renderer.appendChild(headerRowElem, columnElem);
          }
        });
      }
  }

  isAnyColumnVisible() : boolean{
    this.options.columns.forEach(item => {
      if (item.isVisible) return true;
    });
    return false;
  }
}
