import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TransportBoxListComponent } from './transport-box-list.component';
import { ITransportBoxData } from '../../models/ITransportBoxData';
import { Guid } from 'guid-typescript';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';

const box1: ITransportBoxData = 
{
  boxCategory: "MEDICAL",
  boxGuid: Guid.create().toString(),
  number: 10,
  description: "DIE BOX",
  locationDeployment: "",
  locationTransport: "",
  locationHome: "",
  projectGuid: Guid.create().toString()
}
const box2: ITransportBoxData = 
{
  boxCategory: "OFFICE",
  boxGuid: Guid.create().toString(),
  number: 15,
  description: "DIE BOX",
  locationDeployment: "",
  locationTransport: "",
  locationHome: "",
  projectGuid: Guid.create().toString()
}
const box3: ITransportBoxData = 
{
  boxCategory: "OFFICE",
  boxGuid: Guid.create().toString(),
  number: 21,
  description: "DIE BOX",
  locationDeployment: "",
  locationTransport: "",
  locationHome: "",
  projectGuid: Guid.create().toString()
}
const box4: ITransportBoxData = 
{
  boxCategory: "OFFICE",
  boxGuid: Guid.create().toString(),
  number: 2,
  description: "DIE BOX",
  locationDeployment: "",
  locationTransport: "",
  locationHome: "",
  projectGuid: Guid.create().toString()
}
describe('TransportBoxListComponent', () => {
  let component: TransportBoxListComponent;
  let fixture: ComponentFixture<TransportBoxListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        TransportBoxListComponent,
        BrowserAnimationsModule, 
        HttpClientTestingModule
      ]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransportBoxListComponent);
    component = fixture.componentInstance;
    component.transportBoxes = [box1, box2, box3, box4];
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('sorting works', () => {
    expect(component.getSortedBoxes()[0]).toEqual(box4);
  });
});
