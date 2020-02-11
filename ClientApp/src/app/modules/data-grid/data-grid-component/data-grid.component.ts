import { Component, OnInit, Renderer2, Input, ElementRef, ViewChild } from '@angular/core'
import { IOptions } from '../interfaces/i-options'
import { IRow } from '../interfaces/i-row'

@Component({
  selector: 'dataGrid',
  templateUrl: './data-grid.component.html',
  styleUrls: ['./data-grid.component.css']
})
export class DataGridComponent implements OnInit {

  constructor(private renderer: Renderer2) { }

  @Input()
  dataSource: IRow[]

  @Input()
  options = <IOptions>{}

  @ViewChild('tableRef', { static: true })
  tableElement: ElementRef

  ngOnInit() {
    if (this.dataSource && this.options?.columnHeaders) {
      let headerRowElem = this.renderer.createElement('tr')
      this.options.columnHeaders.forEach((column, index) => {
        if (column.isVisible) {
          let columnElem = this.renderer.createElement('th')
          if (column.displayName) {
            columnElem.innerText = columnElem.displayName
          }
          else if (column.name) {
            columnElem.innerText = column.name
          }
          else if (this.dataSource[0][index]) {
            columnElem.innerText = this.dataSource[0][index].name
          }
          else {
            columnElem.innerText = index.toString()
          }
          this.renderer.appendChild(headerRowElem, columnElem)
          this.renderer.appendChild(this.tableElement, headerRowElem)
        }
      })
      this.dataSource.forEach(row => {
        let rowElem = this.renderer.createElement('tr')
        row.cells.forEach((cell, index) => {
          let columnHeaders = this.options.columnHeaders
          if (cell.name == columnHeaders[index].name &&
            columnHeaders[index].isVisible) {
            let cellElem = this.renderer.createElement('td')
            cellElem.innerText = cell.value
            this.renderer.appendChild(rowElem, cellElem)
          }
        })
        this.renderer.appendChild(this.tableElement, rowElem)
      })
    }
  }
}
