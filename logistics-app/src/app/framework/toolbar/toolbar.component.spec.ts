import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToolbarComponent } from './toolbar.component';
import { IToolbarButton } from '../../models/IToolbarButton';
import { Guid } from 'guid-typescript';

const toolbarButton1: IToolbarButton = {
  id: Guid.create().toString(),
  icon: ""
}

describe('ToolbarComponent', () => {
  let component: ToolbarComponent;
  let fixture: ComponentFixture<ToolbarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ToolbarComponent]
    });
    fixture = TestBed.createComponent(ToolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('add item', () => {
    component.addToolbarButton(toolbarButton1);
    expect(component.toolbarButtons.length).toEqual(1);
  });
});
