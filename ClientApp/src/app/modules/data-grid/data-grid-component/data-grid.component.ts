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
    this.InitializeOptions()
    this.createColumnHeaders()
    this.createContent()
  }

  InitializeOptions() {
    if (this.dataSource) {
      if (this.options === undefined) {
        this.options = <IOptions>{}
        this.options.isRadOnly = true
        this.options.rowsPerPage = 10
      }
    }
  }

  createColumnHeaders() {
    let headerRowElem = this.renderer.createElement('tr')
    if (this.options.columnHeaders) {
      this.options.columnHeaders.forEach((column) => {
        let columnElem = this.renderer.createElement('th')
        if (column.displayName) {
          columnElem.innerText = column.displayName
          this.renderer.addClass(columnElem, column.name)
        }
        else if (column.name) {
          columnElem.innerText = column.name
          this.renderer.addClass(columnElem, column.name)
        }
        if (column.isVisible === false)
          columnElem.style.visibility = 'hidden'

        this.renderer.appendChild(headerRowElem, columnElem)
        this.renderer.appendChild(
          this.tableElement.nativeElement, headerRowElem)
      })
    } else {
      this.dataSource[0].cells.forEach(cell => {
        let cellElem = this.renderer.createElement('th')
        cellElem.innerText = cell.name
        this.renderer.addClass(cellElem, cell.name)
        this.renderer.appendChild(headerRowElem, cellElem)
      })
      this.renderer.appendChild(
        this.tableElement.nativeElement, headerRowElem)
    }
  }

  createContent() {
    this.dataSource.forEach(row => {
      let rowElem = this.renderer.createElement('tr')
      let headerRowElem = this.tableElement.nativeElement.firstChild

      row.cells.forEach((cell, index) => {
        let columnHeaders = this.options.columnHeaders
        let cellElem = this.renderer.createElement('td')
        if (this.hasClass(headerRowElem.childNodes[index], cell.name)) {
          cellElem.innerText = cell.value

          if (!columnHeaders[index].isVisible)
            cellElem.style.visibility = 'hidden'

          this.renderer.appendChild(rowElem, cellElem)
        }
      })
      this.renderer.appendChild(this.tableElement.nativeElement, rowElem)
    })
  }

  hasClass(elem: any, cssClass: string): boolean {
    let result = false;
    elem.classList.forEach(f => {
      if (f === cssClass) result = true
    });
    return result;
  }
}