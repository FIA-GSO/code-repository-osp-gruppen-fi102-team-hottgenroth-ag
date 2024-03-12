import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ViewChild, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { ToolbarComponent } from './framework/toolbar/toolbar.component';
import { NavigationRailComponent } from './framework/navigation-rail/navigation-rail.component';
import { FrameworkService } from './services/framework.service';
import { AuthService } from './services/authentication/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, ToolbarComponent, NavigationRailComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements AfterViewInit
{
  @ViewChild("navRail") navRail!: NavigationRailComponent;
  @ViewChild("toolbar") toolbar!: ToolbarComponent;
  
  private _framework: FrameworkService = inject(FrameworkService);
  private _loginService: AuthService = inject(AuthService);
  private _router: Router = inject(Router);


  ngAfterViewInit(): void
  {
    this._framework.initializeComponents(this.toolbar,this.navRail);

    if(!!this._framework.toolbar)
    {
      this._framework.toolbar.title = "Logistics"
    
    }

    let hasToken: boolean = this._loginService.hasToken();
    let isTokenValid: boolean = this._loginService.isTokenValid();

    if(!hasToken || !isTokenValid)
    {
      this._router.navigate(["./login"]);
      return;
    }
  }
}
