import { CommonModule } from '@angular/common';
import { Component, Inject, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatListModule, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { ITransportBoxData } from '../../models/ITransportBoxData';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { IArticleData } from '../../models/IArticleData';

@Component({
  selector: 'add-article-selection-list',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatListModule, 
    MatButtonModule, MatIconModule],
  templateUrl: './add-article-selection-list.component.html',
  styleUrl: './add-article-selection-list.component.scss'
})
export class AddArticleSelectionListComponent {
  // Alle ausgewählten article werden gespeichert
  public selectedArticleList: IArticleData[] = [];

  // Alle article werden in den Dialog reingegeben
  constructor(@Inject(MAT_DIALOG_DATA) public articles: IArticleData[]){}

  // Alle ausgewählten article werden gesetzt
  public selectionChanged(event: MatSelectionListChange): void
  {
    this.selectedArticleList = event.source.selectedOptions.selected.map(o => o.value);
  }
}
