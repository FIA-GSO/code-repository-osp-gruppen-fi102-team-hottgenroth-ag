import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavigationRailComponent } from './navigation-rail.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { INavRailItem } from '../../models/INavRailItem';
import { Guid } from 'guid-typescript';

const navItem1: INavRailItem = {
  name: "Button",
  identifier: Guid.create().toString(),
  icon: "eye",
  click: () => {

  }
}

describe('NavigationRailComponent', () => {
  let component: NavigationRailComponent;
  let fixture: ComponentFixture<NavigationRailComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        NavigationRailComponent,
        BrowserAnimationsModule
      ]
    });
    fixture = TestBed.createComponent(NavigationRailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('add item and delete', () => {
    component.addNavRailItem(navItem1);
    expect(component.navRailItems.length).toEqual(1);

    component.deleteNavRailItem(navItem1.identifier);
    expect(component.navRailItems.length).toEqual(0);
  });

  it('add item and clear', () => {
    component.addNavRailItem(navItem1);
    expect(component.navRailItems.length).toEqual(1);

    component.clean();
    expect(component.navRailItems.length).toEqual(0);
  });
});
