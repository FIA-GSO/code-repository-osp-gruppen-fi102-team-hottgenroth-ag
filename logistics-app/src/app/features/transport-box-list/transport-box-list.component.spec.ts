import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransportBoxListComponent } from './transport-box-list.component';

describe('TransportBoxListComponent', () => {
  let component: TransportBoxListComponent;
  let fixture: ComponentFixture<TransportBoxListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransportBoxListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransportBoxListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
