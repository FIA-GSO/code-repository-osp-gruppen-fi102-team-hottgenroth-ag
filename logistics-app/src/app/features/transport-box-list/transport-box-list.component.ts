import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';
import { ITransportBoxData } from '../../models/ITransportBoxData';
import { LogisticsStoreService } from '../../services/stores/logistics-store.service';
import { LoadingSpinnerService } from '../../services/loading-spinner.service';
import { TruncatePipe } from '../../framework/TruncatePipe';
import { IProjectData } from '../../models/IProjectData';

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
  
  private _spinner: LoadingSpinnerService = inject(LoadingSpinnerService);
  private _logisticStore: LogisticsStoreService = inject(LogisticsStoreService);
  
  private _selectedBox: ITransportBoxData | undefined;
  public set selectedBox(pBox: ITransportBoxData | undefined)
  {
    if(this._selectedBox != pBox)
    {
      this._selectedBox = pBox;
  
      this._spinner.show("Transportbox is loading...", new Promise<void>(async(resolve, reject) => {
        if(!!this._selectedBox)
        {
          await this._logisticStore.loadArticleForBox(this._selectedBox.boxGuid);
          this.boxSelection.emit(this._selectedBox);
          resolve();
        }
        else reject();
      }));
    }
  }

  public getSortedBoxes(): ITransportBoxData[]
  {
    return this.transportBoxes.sort((a, b) => a.number - b.number);
  }

  public get selectedBox(): ITransportBoxData | undefined
  {
    return this._selectedBox;
  }

  public changeTab()
  {
    this.tabChange.emit();
  }

  public deleteBoxFromProject(): void
  {
    this._spinner.show("deleting transportbox...", new Promise<void>(async(resolve, reject) => {
      if(!!this._selectedBox)
      {
        await this._logisticStore.transportboxStore.delete(this._selectedBox.boxGuid);
        this._selectedBox = undefined;
        this.boxSelection.emit(this._selectedBox);

        this.transportBoxes = this._logisticStore.transportboxStore.getItems();
        resolve();
      }
      else reject();
    }))
  }
}
