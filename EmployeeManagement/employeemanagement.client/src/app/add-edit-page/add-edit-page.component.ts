import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeModel } from '../../models/employee.model';

@Component({
  selector: 'app-add-edit-page',
  templateUrl: './add-edit-page.component.html',
  styleUrls: ['./add-edit-page.component.css'],
})
export class AddEditPageComponent implements OnInit {
  employeeForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<AddEditPageComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.employeeForm = this.fb.group({
      email: [
        '',
        [Validators.required, Validators.email, Validators.maxLength(255)],
      ],
      name: ['', [Validators.required, Validators.maxLength(256)]],
      gender: [''],
      phone: [''],
      dob: ['', Validators.required],
      address: [''],
    });
  }

  ngOnInit(): void {}

  onFormSubmit() {
    if (this.employeeForm.valid) {
      const employeeData: EmployeeModel = this.employeeForm.value;
      this.addEmployee(employeeData);
    } else {
      console.log('Error: Form is invalid');
      // Log the validity status of each form control
      Object.keys(this.employeeForm.controls).forEach((key) => {
        console.log(`${key} validity:`, this.employeeForm.get(key)!.valid);
      });
    }
  }

  addEmployee(employeeData: EmployeeModel) {
    this.http
      .post<EmployeeModel>(
        'https://localhost:7142/Employee/add-employee',
        employeeData
      )
      .subscribe(
        (response) => {
          console.log('Employee added successfully:', response);
          this.snackBar.open('Employee added successfully', 'Close', {
            duration: 3000,
          });
          this.dialogRef.close();
        },
        (error: HttpErrorResponse) => {
          console.error('Error adding employee:', error);
          if (error.status === 400 && error.error === 'Email already exists') {
            this.snackBar.open('Email already exists', 'Close', {
              duration: 3000,
            });
          } else {
            this.snackBar.open('Error adding employee', 'Close', {
              duration: 3000,
            });
          }
        }
      );
  }
}
