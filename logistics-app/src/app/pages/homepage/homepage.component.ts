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

      //Wir prüfen ob man eingeloggt ist und ob es noch gültig ist
      if(!hasToken || !isTokenValid)
      {
        //Sonst leiten wir zur Loginmaske
        this._router.navigate(["./login"]);
        reject();
        return;
      }
  
      if(!!this._framework.toolbar)
      {
        //Wir setzen die Toolbarbuttons je nach Rolle
        if(this.isAuthorized(this._login.getUserRole()))
        {
          this._framework.toolbar.addToolbarButton(this._btnStore.userButton);
          this._framework.toolbar.addToolbarButton(this._btnStore.exportButton);
        }
        this._framework.toolbar.addToolbarButton(this._btnStore.logoutButton);
      }
  
      //Wir adden die NavRail Buttons
      if(!!this._framework.navigationRail)
      {
        //Aktuell benötigen wir diese nicht mehr, sollte die Anwendung aber wachsen, wäre diese
        //wieder sinnig
        this._framework.navigationRail.clean()
        this._framework.navigationRail.addNavRailItem(this._btnStore.projectButton);
        this._framework.navigationRail.addNavRailItem(this._btnStore.boxButton);
      }
  
      //Wir laden alle Projekte und leiten auf die Projektmaske weiter
      await this._logisticsStore.loadIntitalData();
      this._router.navigate(["./projects"])
      resolve();
    }));
  }

  //WIr überprüfen ob der User die benötigten Rechte hat
  private isAuthorized(role: string): boolean
  {
    return role == eRole.admin || role == eRole.keeper || role == eRole.leader
  }
}
