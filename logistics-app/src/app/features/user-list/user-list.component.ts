import { CommonModule } from '@angular/common';
import { Component, Input, inject } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatSelectModule } from '@angular/material/select';
import { IUserData } from '../../models/IUserData';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { eRole } from '../../models/enum/eRole';
import { AuthService } from '../../services/authentication/auth.service';
import { ColorMixerService } from '../../services/color-mixer.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'user-list',
  standalone: true,
  imports: [CommonModule, MatSelectModule, MatDividerModule, MatFormFieldModule, FormsModule,
     MatButtonModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss'
})
export class UserListComponent {
  @Input() roleOptions!: eRole[];
  @Input() userList: IUserData[] = [];

  private _auth: AuthService = inject(AuthService);
  private _colorMixer: ColorMixerService = inject(ColorMixerService);

  constructor(){}

  public updateRole(user: IUserData)
  {
    //Wir ändern die User rolle in der DB
    this._auth.updateUserRole(user);
  }

  //Wir holen aus der E-Mail die Initialien des Users
  public getInitials(mail: string): string
  {
    return this._colorMixer.getInitialsFromEmail(mail);
  }

  //Wir holen zurvor definierte Farben anhand der E-Mail
  public getColor(pEmail: string): Object 
  {
    var backgroundColor = this._colorMixer.getColourByEmail(pEmail);
    //Wir setzen die Schriftfarbe auf eine Farbe mit hohem Kontrast zum Hintergrund
    var val = { background: backgroundColor, color: this.getConstrastColor(backgroundColor) };
    return val;
  }

  //Wir holen eine Kontrastfarbe, damit ein möglichst hoher Kontrast zum Hintergrund ensteht
  private getConstrastColor(color: string): string 
  {
    return this._colorMixer.setContrast(color);
  }
}
