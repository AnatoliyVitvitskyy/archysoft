import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material';
import { EmployeesDataSource } from '../../services/employees.datasource';
import { EmsployeeService } from '../../services/employee.service';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'employees-grid',
  templateUrl: './employees-grid.component.html',
  styleUrls: ['./employees-grid.component.scss']
})
export class EmployeesGridComponent implements OnInit {
  displayedColumns: string[] = ['id', 'userName', 'firstName', 'lastName', 'email'];
  dataSource: EmployeesDataSource;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private employeeService: EmsployeeService) { }

  ngOnInit() {
    this.dataSource = new EmployeesDataSource(this.employeeService);
    this.dataSource.loadEmployees({ search: '', orderBy: '', pageIndex: 1, pageSize: 2 });
  }

}
