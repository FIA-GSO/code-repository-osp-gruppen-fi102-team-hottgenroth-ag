import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransportBoxPageComponent } from './transport-box-page.component';

describe('TransportBoxPageComponent', () => {
  let component: TransportBoxPageComponent;
  let fixture: ComponentFixture<TransportBoxPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransportBoxPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransportBoxPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
