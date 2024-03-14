import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBoxSelectionListComponent } from './AddBoxSelectionListComponent';

describe('AddBoxSelectionListComponent', () => {
  let component: AddBoxSelectionListComponent;
  let fixture: ComponentFixture<AddBoxSelectionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddBoxSelectionListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddBoxSelectionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
