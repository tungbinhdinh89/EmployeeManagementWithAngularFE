import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddEditPageComponent } from './add-edit-page/add-edit-page.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'employee-management-app';

  constructor(private _dialog: MatDialog) {}

  openAddEditForm() {
    this._dialog.open(AddEditPageComponent);
  }
}
