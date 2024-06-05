import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeModel } from '../../models/employee.model';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  constructor(private _http: HttpClient) {}

  addEmployee(data: EmployeeModel): Observable<any> {
    return this._http.post(
      'https://localhost:7142/Employee/add-employee',
      data
    );
  }

  getEmployee(id: number): Observable<any> {
    return this._http.get(`https://localhost:7142/Employee/get-employee/${id}`);
  }

  getEmployeeList(): Observable<any> {
    return this._http.get('https://localhost:7142/Employee/get-employees');
  }
}
