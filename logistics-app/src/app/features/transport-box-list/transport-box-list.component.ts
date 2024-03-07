import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';
import { ITransportBoxData } from '../../models/ITransportBoxData';
import { LogisticsStoreService } from '../../services/stores/logistics-store.service';
import { LoadingSpinnerService } from '../../services/loading-spinner.service';
import { TruncatePipe } from '../../framework/TruncatePipe';

@Component({
  selector: 'transport-box-list',
  standalone: true,
  imports: [CommonModule, MatListModule, MatButtonModule, MatIconModule, TruncatePipe],
  templateUrl: './transport-box-list.component.html',
  styleUrl: './transport-box-list.component.scss'
})
export class TransportBoxListComponent {
  @Output() boxSelection: EventEmitter<ITransportBoxData> = new EventEmitter<ITransportBoxData>();
  @Output() tabChange: EventEmitter<void> = new EventEmitter<void>();

  @Input() public transportBoxes: ITransportBoxData[] = [];
  
  public _selectedBox: ITransportBoxData | undefined;

  private _spinner: LoadingSpinnerService = inject(LoadingSpinnerService);

  public set selectedBox(pBox: ITransportBoxData | undefined)
  {
    if(this._selectedBox != pBox)
    {
      this._selectedBox = pBox;
  
      this._spinner.show("Transportbox is loading...", new Promise<void>(async(resolve, reject) => {
        if(!!this._selectedBox)
        {
          this.boxSelection.emit(this._selectedBox);
          resolve();
        }
      }));
    }
  }

  public get selectedBox(): ITransportBoxData | undefined
  {
    return this._selectedBox;
  }

  public changeTab()
  {
    this.tabChange.emit();
  }
}
