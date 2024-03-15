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
  //Wir definieren Events für die Parentkomponente
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
  
      //Eine Box wurde ausgewählt
      this._spinner.show("Transportbox is loading...", new Promise<void>(async(resolve, reject) => {
        if(!!this._selectedBox)
        {
          //Wir laden alle Artikel für die Box
          await this._logisticStore.loadArticleForBox(this._selectedBox.boxGuid);
          //Wir geben die selektierte Box in die Parentkomponente
          this.boxSelection.emit(this._selectedBox);
          resolve();
        }
        else reject();
      }));
    }
  }

  //Wir sortieren die Boxen anhand der Number
  public getSortedBoxes(): ITransportBoxData[]
  {
    return this.transportBoxes.sort((a, b) => a.number - b.number);
  }

  public get selectedBox(): ITransportBoxData | undefined
  {
    return this._selectedBox;
  }

  //Der change Tab button wurde gedrückt, deswegen 
  //melden wir der Parentkomponente, dass Sie den Tab wechseln soll
  public changeTab()
  {
    this.tabChange.emit();
  }

  //Wir löschen eine Transportbox von einem Projekt und refreshen dann die Liste
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
