import { CommonModule } from '@angular/common';
import { Component, Input, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ITransportBoxData } from '../../models/ITransportBoxData';
import { LogisticsStoreService } from '../../services/stores/logistics-store.service';
import { AuthService } from '../../services/authentication/auth.service';
import { eRole } from '../../models/enum/eRole';

@Component({
  selector: 'transport-box-details',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatFormFieldModule, MatSelectModule, MatInputModule],
  templateUrl: './transport-box-details.component.html',
  styleUrl: './transport-box-details.component.scss'
})
export class TransportBoxDetailsComponent {
  //Die, in der Parentkomponente, selektiere Transportbox
  @Input() selectedBox: ITransportBoxData | undefined;

  private _logisticStore: LogisticsStoreService = inject(LogisticsStoreService);
  private _auth: AuthService = inject(AuthService);

  //Wir definieren Setter, um die Box upzudaten
  public set description(pText: string)
  {
    if(!!this.selectedBox)
    {
      this.selectedBox.description = pText;
      this._logisticStore.transportboxStore.update(this.selectedBox.boxGuid);
    }
  }
  public get description(): string
  {
    if(!!this.selectedBox && !!this.selectedBox.description)
    {
      return this.selectedBox.description;
    }
    return "";
  }

  //Wir definieren Setter, um die Box upzudaten
  public set number(pNumber: number)
  {
    if(!!this.selectedBox)
    {
      this.selectedBox.number = pNumber;
      this._logisticStore.transportboxStore.update(this.selectedBox.boxGuid);
    }
  }
  public get number(): number
  {
    if(!!this.selectedBox && !!this.selectedBox.number)
    {
      return this.selectedBox.number;
    }
    return 0;
  }

  //Wir definieren Setter, um die Box upzudaten
  public set locationHome(pLocation: string)
  {
    if(!!this.selectedBox)
    {
      this.selectedBox.locationHome = pLocation;
      this._logisticStore.transportboxStore.update(this.selectedBox.boxGuid);
    }
  }
  public get locationHome(): string
  {
    if(!!this.selectedBox && !!this.selectedBox.locationHome)
    {
      return this.selectedBox.locationHome
    }
    return "";
  }

  //Wir definieren Setter, um die Box upzudaten
  public set locationTransport(pLocation: string)
  {
    if(!!this.selectedBox)
    {
      this.selectedBox.locationTransport = pLocation;
      this._logisticStore.transportboxStore.update(this.selectedBox.boxGuid);
    }
  }
  public get locationTransport(): string
  {
    if(!!this.selectedBox && !!this.selectedBox.locationTransport)
    {
      return this.selectedBox.locationTransport
    }
    return "";
  }

  //Wir definieren Setter, um die Box upzudaten
  public set locationDeployment(pLocation: string)
  {
    if(!!this.selectedBox)
    {
      this.selectedBox.locationDeployment = pLocation;
      this._logisticStore.transportboxStore.update(this.selectedBox.boxGuid);
    }
  }

  public get locationDeployment(): string
  {
    if(!!this.selectedBox && !!this.selectedBox.locationDeployment)
    {
      return this.selectedBox.locationDeployment
    }
    return "";
  }

  //Wir überprüfen ob der User die passenden Rechte hat
  public isAuthorized(): boolean
  {
    let role: string = this._auth.getUserRole();

    if(role == eRole.admin || role == eRole.keeper || role == eRole.leader)
    {
      return true;
    }
    return false;
  }

  //Wir überprüfen ob der User readOnly ist
  public isReadOnly(): boolean
  {
    let role: string = this._auth.getUserRole();
    return role == eRole.user;
  }
}
