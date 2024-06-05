import { Component, OnInit } from '@angular/core';
import { EmployeeModel } from '../../../models/employee.model';
import { EmployeeService } from '../../services/employee.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css'],
})
export class EmployeeDetailComponent implements OnInit {
  employee: EmployeeModel | null = null;

  constructor(
    private _employeeService: EmployeeService,
    private _route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this._route.paramMap.subscribe((params) => {
      const id = +params.get('id')!;
      this.getEmployee(id);
    });
  }

  getEmployee(id: number) {
    this._employeeService.getEmployee(id).subscribe({
      next: (res) => {
        this.employee = res;
      },
      error: console.log,
    });
  }
}
