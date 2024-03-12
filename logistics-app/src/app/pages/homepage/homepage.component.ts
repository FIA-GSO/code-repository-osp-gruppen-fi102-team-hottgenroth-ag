import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AuthService } from '../../services/authentication/auth.service';
import { Router, RouterModule } from '@angular/router';
import { ButtonStoreService } from '../../services/stores/button-store.service';
import { FrameworkService } from '../../services/framework.service';
import { LoadingSpinnerService } from '../../services/loading-spinner.service';
import { LogisticsStoreService } from '../../services/stores/logistics-store.service';
import { eRole } from '../../models/enum/eRole';

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.scss'
})
export class HomepageComponent {
  private _logisticsStore: LogisticsStoreService = inject(LogisticsStoreService);
  private _btnStore: ButtonStoreService = inject(ButtonStoreService);
  private _framework: FrameworkService = inject(FrameworkService);
  private _login: AuthService = inject(AuthService);
  private _router: Router = inject(Router);
  private _spinner: LoadingSpinnerService = inject(LoadingSpinnerService);

  constructor()
  {
    this._spinner.show("Please wait...", new Promise<void>(async(resolve, reject) => {
      let hasToken: boolean = this._login.hasToken();
      let isTokenValid: boolean = this._login.isTokenValid();
  
      if(!hasToken || !isTokenValid)
      {
        this._router.navigate(["./login"]);
        reject();
        return;
      }
  
      if(!!this._framework.toolbar)
      {
        if(this.isAuthorized(this._login.getUserRole()))
        {
          this._framework.toolbar.addToolbarButton(this._btnStore.userButton);
        }
        this._framework.toolbar.addToolbarButton(this._btnStore.logoutButton);
      }
  
      if(!!this._framework.navigationRail)
      {
        this._framework.navigationRail.clean()
        this._framework.navigationRail.addNavRailItem(this._btnStore.projectButton);
        this._framework.navigationRail.addNavRailItem(this._btnStore.boxButton);
      }
  
      await this._logisticsStore.loadIntitalData();
      this._router.navigate(["./projects"])
      resolve();
    }));
  }


  private isAuthorized(role: string): boolean
  {
    return role == eRole.admin || role == eRole.keeper || role == eRole.leader
  }
}
