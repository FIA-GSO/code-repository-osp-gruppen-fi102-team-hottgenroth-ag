import { CommonModule } from '@angular/common';
import { Component, Input, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { IUserData } from '../../models/IUserData';
import { ColorMixerService } from '../../services/color-mixer.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'user-details',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.scss'
})
export class UserDetailsComponent {
  @Input() public userData!: IUserData;

  private _colorMixer: ColorMixerService = inject(ColorMixerService);
  constructor(){}

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
