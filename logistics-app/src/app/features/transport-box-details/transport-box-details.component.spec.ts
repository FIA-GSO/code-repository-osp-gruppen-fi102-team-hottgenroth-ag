import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransportBoxDetailsComponent } from './transport-box-details.component';

describe('TransportBoxDetailsComponent', () => {
  let component: TransportBoxDetailsComponent;
  let fixture: ComponentFixture<TransportBoxDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransportBoxDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransportBoxDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
