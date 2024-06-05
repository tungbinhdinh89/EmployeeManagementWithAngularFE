import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  constructor(private _http: HttpClient) {}

  addEmployee(data: any): Observable<any> {
    return this._http.post(
      'https://localhost:7142/Employee/add-employee',
      data
    );
  }

  updateEmployee(id: number, data: any): Observable<any> {
    return this._http.put(`http://localhost:3000/employees/${id}`, data);
  }

  getEmployee(id: number): Observable<any> {
    return this._http.get(`https://localhost:7142/Employee/get-employee${id}`);
  }

  getEmployeeList(): Observable<any> {
    return this._http.get('https://localhost:7142/Employee/get-employees');
  }

  deleteEmployee(id: number): Observable<any> {
    return this._http.delete(`http://localhost:3000/employees/${id}`);
  }
}
