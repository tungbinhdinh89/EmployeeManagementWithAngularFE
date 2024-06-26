import { Component, Inject, OnInit, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeService } from '../services/employee.service';

@Component({
  selector: 'app-add-edit-page',
  templateUrl: './add-edit-page.component.html',
  styleUrls: ['./add-edit-page.component.css'],
})
export class AddEditPageComponent implements OnInit {
  @Output() employeeAdded = new EventEmitter<void>();
  employeeForm: FormGroup;
  emailExistError: boolean = false;

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private _dialogRef: MatDialogRef<AddEditPageComponent>,
    private _employeeService: EmployeeService,
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
      this._employeeService.addEmployee(this.employeeForm.value).subscribe({
        next: (val: any) => {
          if (!val.success) {
            if (val.code === 'EmailExist') {
              this.emailExistError = true;
            }
          } else {
            this.snackBar.open('Employee added successfully', 'Close', {
              duration: 3000,
            });
            this.employeeAdded.emit();
            this._dialogRef.close(true);
          }
        },
      });
    } else {
      console.log('Error: Form is invalid');
      // Log the validity status of each form control
      Object.keys(this.employeeForm.controls).forEach((key) => {
        console.log(`${key} validity:`, this.employeeForm.get(key)!.valid);
      });
    }
  }
}
