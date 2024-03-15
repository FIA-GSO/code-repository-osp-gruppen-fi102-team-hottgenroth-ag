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
  @ViewChild("selectlist") selectionList!: MatSelectionList
  public selectedBoxList: ITransportBoxData[] = [];

  private allSelected: boolean = false;
  constructor(@Inject(MAT_DIALOG_DATA) public transportBoxes: ITransportBoxData[]){}

  public selectionChanged(event: MatSelectionListChange): void
  {
    console.log(event)
    this.selectedBoxList = event.source.selectedOptions.selected.map(o => o.value);
  }

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
      this.selectedBoxList = this.selectionList.selectedOptions.selected.map(o => o.value);
      this.allSelected = !this.allSelected;
    }
  }
}
