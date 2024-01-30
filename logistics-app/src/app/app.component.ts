import { CommonModule } from '@angular/common';
import { Component, ViewChild, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToolbarComponent } from './framework/toolbar/toolbar.component';
import { NavigationRailComponent } from './framework/navigation-rail/navigation-rail.component';
import { FrameworkService } from './services/framework.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, ToolbarComponent, NavigationRailComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent 
{
  @ViewChild("navRail") navRail!: NavigationRailComponent;
  @ViewChild("toolbar") toolbar!: ToolbarComponent;
  
  private _framework: FrameworkService = inject(FrameworkService);


  ngAfterViewInit(): void
  {
    this._framework.initializeComponents(this.toolbar,this.navRail);

    if(!!this._framework.toolbar)
      this._framework.toolbar.title = "Logistics"

    if(!!this._framework.navigationRail)
    {
      this._framework.navigationRail.addNavRailItem("Home", "/");
    }
  }
}
