import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeesRoutingModule } from './employees-routing.module';
import { EmployeesComponent } from './employees.component';
import { SharedModule } from '../shared/shared.module';
import { EmployeesGridComponent } from './components/employees-grid/employees-grid.component';

@NgModule({
  declarations: [EmployeesComponent, EmployeesGridComponent],
  imports: [
    CommonModule,
    EmployeesRoutingModule,
    SharedModule
  ]
})
export class EmployeesModule { }
