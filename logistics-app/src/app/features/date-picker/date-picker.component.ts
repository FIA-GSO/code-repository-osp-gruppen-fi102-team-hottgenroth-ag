import { Component, EventEmitter, Injectable, Input, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { DateAdapter, MatNativeDateModule, NativeDateAdapter } from '@angular/material/core';
import { DatePipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Injectable()
export class YearDateAdapter extends NativeDateAdapter
{
  override format(date: Date, displayFormat: Object)
  {
    return date.getDate() + "." + date.getMonth() + "." + date.getFullYear()
  }
}

@Component({
  selector: 'date-picker',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDatepickerModule, MatFormFieldModule, MatInputModule, MatNativeDateModule, MatButtonModule],
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.scss'],
  providers:
  [
    {
      provide: DateAdapter,
      useClass: YearDateAdapter
    },
  ]
})
export class DatePickerComponent
{
  private _datePipe: DatePipe = inject(DatePipe);

  private _date!: string;
  @Input() set date(date: string)
  {
    if(date == null) return;

    let parsedDate = this._datePipe.transform(date,"yyyy-MM-dd");
    this._date = !!parsedDate ? parsedDate : date;
  }

  public get date(): string
  {
    return this._date;
  }

  @Input() title!: string;

  @Output() dateChanged: EventEmitter<string> = new EventEmitter<string>();

  constructor()
  {}

  public setDate(value: string)
  {
    let date = new Date(value);
    this.date = date.toDateString();
    this.dateChanged.emit(this.date);
  }
}

