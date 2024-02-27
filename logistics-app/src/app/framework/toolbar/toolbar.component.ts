import { ChangeDetectorRef, Component, EventEmitter, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import { IToolbarButton } from '../../models/IToolbarButton';


@Component({
  selector: 'app-toolbar',
  standalone: true,
  imports: [CommonModule, MatToolbarModule, MatButtonModule, MatIconModule, MatMenuModule],
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent {
  @Output() navRailToggled: EventEmitter<void> = new EventEmitter<void>();

  private _cd: ChangeDetectorRef = inject(ChangeDetectorRef);

  public height: number = 70;

  private _title: string = "";
  public set title(pTitle: string)
  {
    this._title = pTitle;

    this._cd.detectChanges();
  }

  public get title()
  {
    return this._title
  }

  private _subtitle: string = "";
  public set subtitle(pSubTitle: string)
  {
    this._subtitle = pSubTitle;

    this._cd.detectChanges();
  }

  public get subtitle()
  {
    return this._subtitle
  }

  public toolbarButtons: IToolbarButton[] = [];

  public addToolbarButton(button: IToolbarButton)
  {
    let index = this.toolbarButtons.findIndex(items => items.id === button.id);
    if(index == -1)
    {
      this.toolbarButtons.push(button);
    }
  }

  public buttonClicked(btn: IToolbarButton)
  {
    if(!!btn.click)
    {
      btn.click();
    }
  }

  public toggleNavRail()
  {
    this.navRailToggled.emit();
  }
}
