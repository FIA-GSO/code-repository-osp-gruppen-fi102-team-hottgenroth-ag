import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNameDialogComponent } from './add-name-dialog.component';

describe('AddNameDialogComponent', () => {
  let component: AddNameDialogComponent;
  let fixture: ComponentFixture<AddNameDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddNameDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddNameDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
