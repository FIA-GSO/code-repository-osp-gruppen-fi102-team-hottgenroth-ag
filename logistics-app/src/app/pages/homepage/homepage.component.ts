import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ProjectStoreService } from '../../services/stores/project-store.service';
import { AuthService } from '../../services/authentication/auth.service';
import { Router, RouterModule } from '@angular/router';
import { ButtonStoreService } from '../../services/stores/button-store.service';
import { FrameworkService } from '../../services/framework.service';

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.scss'
})
export class HomepageComponent {
  private _projectStore: ProjectStoreService = inject(ProjectStoreService);
  private _btnStore: ButtonStoreService = inject(ButtonStoreService);
  private _framework: FrameworkService = inject(FrameworkService);
  private _login: AuthService = inject(AuthService);
  private _router: Router = inject(Router);

  constructor()
  {
    let hasToken: boolean = this._login.hasToken();
    let isTokenValid: boolean = this._login.isTokenValid();

    if(!hasToken || !isTokenValid)
    {
      this._router.navigate(["./login"]);
      return;
    }

    if(!!this._framework.toolbar)
    {
      this._framework.toolbar.addToolbarButton(this._btnStore.loginButton)
    }
    
    this._projectStore.loadIntitalData();

  }
}
