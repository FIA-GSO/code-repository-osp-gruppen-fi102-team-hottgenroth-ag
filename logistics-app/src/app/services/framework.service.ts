import { Injectable } from '@angular/core';
import { Subscription } from 'rxjs';
import { NavigationRailComponent } from '../framework/navigation-rail/navigation-rail.component';
import { ToolbarComponent } from '../framework/toolbar/toolbar.component';
import { IToolbarButton } from '../models/IToolbarButton';



@Injectable({
  providedIn: 'root'
})
export class FrameworkService {

  // NavigationRails
  private _navigationRail!: NavigationRailComponent | undefined;
  public get navigationRail(): NavigationRailComponent | undefined
  {
    return this._navigationRail;
  }

  private set navigationRail(rail: NavigationRailComponent | undefined)
  {
    if(!!this._navigationRail)
    {
      if(this._navigationRail.isOpen)
      {
        this._navigationRail.toggle();
      }
    }

    this._navigationRail = rail;
  }

    // Toolbar
    private _toggleClickedSubscription!: Subscription;
    private _toolbar!: ToolbarComponent | undefined;
    public get toolbar(): ToolbarComponent | undefined
    {
      return this._toolbar;
    }

    private set toolbar(bar: ToolbarComponent | undefined)
    {
      this._toolbar = bar;
    }

  /////////////////
  // Initializer //
  /////////////////
  public initializeComponents(toolbar: ToolbarComponent | undefined,navigationRail: NavigationRailComponent | undefined): void
  {
    this.toolbar = toolbar;
    this.navigationRail = navigationRail;

    if(!!this._toolbar)
    {
      if(!!this._toggleClickedSubscription)
      {
        this._toggleClickedSubscription.unsubscribe();
      }

      this._toggleClickedSubscription = this._toolbar.navRailToggled.subscribe(() => {
        if(!!this._navigationRail)
        {
          this._navigationRail.toggle();
        }
      })
    }
  }

  public createToolbarButton(icon: string, id: string, text?: string, click?: () => void)
  {
    var button: IToolbarButton =
    {
      icon: icon,
      id: id,
      text: text,
      click: click
    }

    return button;
  }
}
