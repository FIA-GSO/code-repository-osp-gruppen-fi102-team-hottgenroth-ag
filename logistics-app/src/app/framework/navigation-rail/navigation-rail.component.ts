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
  private _router: Router = inject(Router);

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

  public async navigateToRoute(pRoute: string): Promise<void>
  {
    await this._router.navigate([pRoute]);
    if(!!this.railNav)
    {
      this.railNav.close();
    }
  }

  public addNavRailItem(pName: string, pRoute: string, pIcon: string | undefined = undefined): void
  {
    var item: INavRailItem = {
      name: pName,
      route: pRoute,
      identifier: Guid.create().toString(),
      icon: pIcon
    }

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
