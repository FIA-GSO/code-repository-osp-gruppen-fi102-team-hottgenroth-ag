import { ComponentFixture, TestBed } from '@angular/core/testing';
import {HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { DatePickerComponent } from './date-picker.component';
import { DatePipe } from '@angular/common';

describe('DatePickerComponent', () => {
  let component: DatePickerComponent;
  let fixture: ComponentFixture<DatePickerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        DatePickerComponent,
        HttpClientTestingModule,
        BrowserAnimationsModule
      ],
      providers:
      [
        DatePipe
      ]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DatePickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('set date', () => {
    component.setDate("2012-02-20")
    expect(component.date).toEqual("2012-02-20");
  });
});
