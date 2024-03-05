import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { UserDetailsComponent } from '../../features/user-details/user-details.component';
import { UserListComponent } from '../../features/user-list/user-list.component';
import { MatCardModule } from '@angular/material/card';
import { AuthService } from '../../services/authentication/auth.service';
import { IUserData } from '../../models/IUserData';

@Component({
  selector: 'app-user-management-page',
  standalone: true,
  imports: [CommonModule, UserDetailsComponent, UserListComponent, MatCardModule],
  templateUrl: './user-management-page.component.html',
  styleUrl: './user-management-page.component.scss'
})
export class UserManagementPageComponent {
  private _authService: AuthService = inject(AuthService);

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
}
