import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeIndexComponent } from './employee-index/employee-index.component';
import { EmployeeDetailComponent } from './employee-detail/employee-detail.component';
import { EmployeeService } from '../services/employee.service';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { EmployeesRoutingModule } from './employees-routing.module';
import { MatCardModule } from '@angular/material/card';

@NgModule({
  declarations: [EmployeeIndexComponent, EmployeeDetailComponent],
  imports: [
    CommonModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    MatInputModule,
    MatFormFieldModule,
    EmployeesRoutingModule,
    MatCardModule,
  ],
  providers: [EmployeeService],
})
export class EmployeesModule {}
