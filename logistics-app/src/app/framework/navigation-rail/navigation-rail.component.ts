import { ChangeDetectorRef, Component, ElementRef, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { INavRailItem } from '../../models/INavRailItem';
import { Guid } from 'guid-typescript';
import { Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-navigation-rail',
  standalone: true,
  imports: [CommonModule, MatSidenavModule, RouterModule, MatButtonModule],
  templateUrl: './navigation-rail.component.html',
  styleUrls: ['./navigation-rail.component.scss']
})
export class NavigationRailComponent
{
  private _cd: ChangeDetectorRef = inject(ChangeDetectorRef);

  @ViewChild('drawer') railNav!: MatSidenav;

  private _navRailItems: INavRailItem[] = [];
  public get navRailItems(): INavRailItem[]
  {
    return this._navRailItems;
  }

  public get isOpen(): boolean
  {
    if(!!this.railNav)
    {
      return this.railNav.opened;
    }
    return false;
  }

  public toggle(): void
  {
    if(!!this.railNav)
    {
      this.railNav.toggle();
    }
  }

  public clean(): void
  {
    this._navRailItems = [];
  }

  public async itemClicked(item: INavRailItem): Promise<void>
  {
    item.click();
    if(!!this.railNav)
    {
      this.railNav.close();
    }
  }

  public addNavRailItem(item: INavRailItem): void
  { 
    this._navRailItems.push(item);
    this._cd.detectChanges();
  }

  public deleteNavRailItem(identifier: string): void
  {
    var index = this._navRailItems.findIndex(item => item.identifier === identifier);

    if(index != -1)
    {
      this._navRailItems.splice(index, 1)
      this._cd.detectChanges();
    }
  }
}
