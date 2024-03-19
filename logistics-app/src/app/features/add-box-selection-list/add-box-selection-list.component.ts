import { CommonModule } from '@angular/common';
import { Component, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatListModule, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { ITransportBoxData } from '../../models/ITransportBoxData';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'add-box-selection-list',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatListModule, 
    MatButtonModule, MatIconModule],
  templateUrl: './add-box-selection-list.component.html',
  styleUrl: './add-box-selection-list.component.scss'
})
export class AddBoxSelectionListComponent {
  //Das Listenelement wird im Code zugänglich gemacht
  @ViewChild("selectlist") selectionList!: MatSelectionList;

  // Alle ausgewählten boxen werden gespeichert
  public selectedBoxList: ITransportBoxData[] = [];

  //Es wird zwischengespeichert ob zuletzt alle Article selektiert/unselektiert wurde
  private allSelected: boolean = false;

  // Alle article werden in den Dialog reingegeben
  constructor(@Inject(MAT_DIALOG_DATA) public transportBoxes: ITransportBoxData[]){}

  // Alle ausgewählten boxen werden gesetzt
  public selectionChanged(event: MatSelectionListChange): void
  {
    this.selectedBoxList = event.source.selectedOptions.selected.map(o => o.value);
  }

  //Es werden alle möglichen Optionen selektiert/deselektiert
  public toggleSelect(): void
  {
    if(!!this.selectionList)
    {
      if(!this.allSelected)
      {
        this.selectionList.selectAll();
      }
      else
      {
        this.selectionList.deselectAll();
      }
      //Wir setzen alles auf die Liste
      this.selectedBoxList = this.selectionList.selectedOptions.selected.map(o => o.value);
      this.allSelected = !this.allSelected;
    }
  }
}
