import { ChangeDetectorRef, Component, ElementRef, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { INavRailItem } from '../../models/INavRailItem';
import { RouterModule } from '@angular/router';
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

  //Wir holen das HTML Element der Sidenav und machen es zugänglich
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

  //Wir öffnen bzw schließen die Sidenav
  public toggle(): void
  {
    if(!!this.railNav)
    {
      this.railNav.toggle();
    }
  }

  //Es werden alle Items geleert
  public clean(): void
  {
    this._navRailItems = [];
  }

  //Wir triggern die item Click Funktion
  public async itemClicked(item: INavRailItem): Promise<void>
  {
    item.click();
    if(!!this.railNav)
    {
      this.railNav.close();
    }
  }

  //Adde das Navigationsitem in die Sidenav
  public addNavRailItem(item: INavRailItem): void
  { 
    this._navRailItems.push(item);
    this._cd.detectChanges();
  }

  //Lösch das Navigationsitem aus der Sidenav
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
