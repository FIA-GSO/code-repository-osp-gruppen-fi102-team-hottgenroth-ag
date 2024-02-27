import { CommonModule } from '@angular/common';
import { Component, EventEmitter, HostListener, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule } from '@angular/forms';
import { IDialogData } from '../../models/IDialogData';

@Component({
  selector: 'app-shared-dialog',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, MatCardModule,
    MatDividerModule, MatDialogModule, 
    MatCheckboxModule, FormsModule],
  templateUrl: './shared-dialog.component.html',
  styleUrls: ['./shared-dialog.component.scss']
})
export class SharedDialogComponent {

  /** positive button clicked */
  public onPositiveButtonClicked: EventEmitter<void> = new EventEmitter<void>();

  /** negative button clicked */
  public onNegativeButtonClicked: EventEmitter<void> = new EventEmitter<void>();

  @HostListener('keyup', ['$event'])
  handleKeyUpEvent(event: KeyboardEvent) {
    if (event.key === 'Escape') {
      if (this._data.hasCancelButton) {
        this.negativeButtonClicked();
      } else {
        this.positiveButtonClicked();
      }
    } else if (event.key === 'Enter') {
      this.positiveButtonClicked();
    }
  }

  private _data: IDialogData;
  public get data(): IDialogData {
    return this._data;
  } 

  constructor(public dialogRef: MatDialogRef<SharedDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public dialogData: IDialogData) {
    this._data = dialogData;
  }

  public positiveButtonClicked(): void {
    this.onPositiveButtonClicked.emit();
    this.dialogRef.close(true);
  }

  public negativeButtonClicked(): void {
    this.onNegativeButtonClicked.emit();
    this.dialogRef.close(false);
  }
}
