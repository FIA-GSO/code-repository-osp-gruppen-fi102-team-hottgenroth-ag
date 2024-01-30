import { ChangeDetectorRef, Component, EventEmitter, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';

@Component({
  selector: 'app-toolbar',
  standalone: true,
  imports: [CommonModule, MatToolbarModule, MatButtonModule, MatIconModule],
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent {
  private _cd: ChangeDetectorRef = inject(ChangeDetectorRef);

  @Output() navRailToggled: EventEmitter<void> = new EventEmitter<void>();
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

  public toggleNavRail()
  {
    this.navRailToggled.emit();
  }
}
