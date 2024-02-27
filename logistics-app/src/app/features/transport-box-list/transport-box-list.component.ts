import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';

@Component({
  selector: 'transport-box-list',
  standalone: true,
  imports: [CommonModule, MatListModule, MatButtonModule, MatIconModule],
  templateUrl: './transport-box-list.component.html',
  styleUrl: './transport-box-list.component.scss'
})
export class TransportBoxListComponent {
  @Output() boxSelection: EventEmitter<string> = new EventEmitter<string>();
  @Output() tabChange: EventEmitter<void> = new EventEmitter<void>();

  public boxList: string[] = ['Boots', 'Clogs', 'Loafers', 'Moccasins', 'Sneakers','Bootss', 'Clogas', 'Loaferds', 'Moccasfins', 'Snseakers'];
  public _selectedBox: string | undefined;


  public set selectedBox(pBox: string | undefined)
  {
    this._selectedBox = pBox;

    if(!!this._selectedBox)
    {
      this.boxSelection.emit(this._selectedBox);
    }
  }

  public get selectedBox(): string | undefined
  {
    return this._selectedBox;
  }

  public changeTab()
  {
    this.tabChange.emit();
  }
}
