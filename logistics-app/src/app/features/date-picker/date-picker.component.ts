import { Component, EventEmitter, Injectable, Input, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { DatePipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Injectable()

@Component({
  selector: 'date-picker',
  standalone: true,
  imports: [CommonModule, FormsModule, MatDatepickerModule, MatFormFieldModule, MatInputModule, MatNativeDateModule, MatButtonModule],
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.scss']
})
export class DatePickerComponent
{
  private _datePipe: DatePipe = inject(DatePipe);

  private _date!: string;
  @Input() set date(date: string)
  {
    if(date == null) return;
    //WIr formatieren das Datum ins richtige Format und setzen es dann
    let parsedDate = this._datePipe.transform(date,"yyyy-MM-dd");
    this._date = !!parsedDate ? parsedDate : date;
  }

  public get date(): string
  {
    return this._date;
  }

  @Input() title!: string;
  //Wir definieren ein Event, an das sich die Parent Komponente dran hängen kann
  @Output() dateChanged: EventEmitter<string> = new EventEmitter<string>();

  constructor()
  {}

  public setDate(value: string)
  {
    //Wir setzen das Datum und triggern das definierte Event
    let date = new Date(value);
    this.date = date.toDateString();
    this.dateChanged.emit(this.date);
  }
}

