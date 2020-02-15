import { Component, OnInit } from '@angular/core';
import { IOptions } from 'src/app/interfaces/i-options';
import { IRow } from 'src/app/interfaces/i-row';

@Component({
  selector: 'app-grid-test',
  templateUrl: './grid-test.component.html',
  styleUrls: ['./grid-test.component.css']
})
export class GridTestComponent implements OnInit {

  constructor() { }

  dataGridOptions: IOptions = {
    isRadOnly: true,
    rowsPerPage: 5,
    columnHeaders: [
      { name: 'ID', displayName: 'ID', isReadOnly: true, isVisible: false },
      { name: 'FirstName', displayName: 'First name', isReadOnly: true, isVisible: true },
      { name: 'LastName', displayName: 'Last name', isReadOnly: true, isVisible: true },
    ]
  }

  dataGridDataSource: IRow[] =[
    { cells: [{ name: 'ID', value: 1 }, { name: 'FirstName', value: 'tomas'}, { name: 'LastName', value: 'Kowalski'}] },
    { cells: [{ name: 'ID', value: 2 }, { name: 'FirstName', value: 'jan'}, { name: 'LastName', value: 'Kwiatkowski'}] }
  ]

  ngOnInit() {
  }

}
