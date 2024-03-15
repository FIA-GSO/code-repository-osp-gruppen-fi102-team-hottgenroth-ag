import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TransportBoxPageComponent } from './transport-box-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('TransportBoxPageComponent', () => {
  
  let component: TransportBoxPageComponent;
  let fixture: ComponentFixture<TransportBoxPageComponent>;
  
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        TransportBoxPageComponent,
        BrowserAnimationsModule,
        HttpClientTestingModule
      ]
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
