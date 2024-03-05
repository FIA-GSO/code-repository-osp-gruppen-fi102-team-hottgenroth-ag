import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { UserListComponent } from '../../features/user-list/user-list.component';
import { MatCardModule } from '@angular/material/card';
import { AuthService } from '../../services/authentication/auth.service';
import { IUserData } from '../../models/IUserData';
import { ColorMixerService } from '../../services/color-mixer.service';
import { MatButtonModule } from '@angular/material/button';

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

  private _user!: IUserData; 
  public get user(): IUserData
  {
    return this._user;
  }

  constructor()
  {
    var user: IUserData = {
      userEmail: this._authService.getUserEmail(),
      userId: this._authService.getUserId(),
      role: this._authService.getUserRole()
    }
    this._user = user;
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
