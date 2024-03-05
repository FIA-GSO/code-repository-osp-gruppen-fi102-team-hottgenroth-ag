import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { UserListComponent } from '../../features/user-list/user-list.component';
import { MatCardModule } from '@angular/material/card';
import { AuthService } from '../../services/authentication/auth.service';
import { IUserData } from '../../models/IUserData';
import { ColorMixerService } from '../../services/color-mixer.service';
import { MatButtonModule } from '@angular/material/button';
import { eRole } from '../../models/enum/eRole';
import { async } from 'rxjs';
import { LoadingSpinnerService } from '../../services/loading-spinner.service';

@Component({
  selector: 'app-user-management-page',
  standalone: true,
  imports: [CommonModule, UserListComponent, MatCardModule, MatButtonModule],
  templateUrl: './user-management-page.component.html',
  styleUrl: './user-management-page.component.scss'
})
export class UserManagementPageComponent {
  private _authService: AuthService = inject(AuthService);
  private _colorMixer: ColorMixerService = inject(ColorMixerService);
  private _spinner: LoadingSpinnerService = inject(LoadingSpinnerService);

  private _user!: IUserData; 
  public get user(): IUserData
  {
    return this._user;
  }

  public get roleOptions(): eRole[]
  {
    switch(this._user.role)
    {
      case eRole.admin:
        return [eRole.admin, eRole.keeper, eRole.leader, eRole.member, eRole.user];
      case eRole.keeper:
        return [eRole.keeper, eRole.leader, eRole.member, eRole.user];
      case eRole.leader: 
        return [eRole.leader, eRole.member, eRole.user];
      default: 
        return [eRole.member, eRole.user];
    }
  }

  private _allUser!: IUserData[];
  public get allUser(): IUserData[]
  {
    return this._allUser
  }     
  
  constructor()
  {
    var user: IUserData = {
      userEmail: this._authService.getUserEmail(),
      userId: this._authService.getUserId(),
      role: this._authService.getUserRole()
    }
    this._user = user;

    this._spinner.show("Loading user...",new Promise<void>(async(resolve, reject) => {
      let user = await this._authService.getAllUser();
      let filteredUser = user.filter(item => this.roleOptions.includes(item.role as eRole) && item.userEmail != this.user.userEmail);
      this._allUser = filteredUser;
      resolve();
    }));
  }

  public getInitials(mail: string): string
  {
    return this._colorMixer.getInitialsFromEmail(mail);
  }

  public getColor(pEmail: string): Object 
  {
    var backgroundColor = this._colorMixer.getColourByEmail(pEmail);
    var val = { background: backgroundColor, color: this.getConstrastColor(backgroundColor) };
    return val;
  }

  private getConstrastColor(color: string): string 
  {
    return this._colorMixer.setContrast(color);
  }
}
