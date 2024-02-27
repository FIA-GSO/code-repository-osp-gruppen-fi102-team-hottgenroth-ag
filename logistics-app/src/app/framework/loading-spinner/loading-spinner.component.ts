import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, inject } from '@angular/core';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { LoadingSpinnerService } from '../../services/loading-spinner.service';

@Component({
  selector: 'app-loading-spinner',
  standalone: true,
  imports: [CommonModule, MatProgressSpinnerModule],
  templateUrl: './loading-spinner.component.html',
  styleUrl: './loading-spinner.component.scss'
})
export class LoadingSpinnerComponent {
  private _cd: ChangeDetectorRef = inject(ChangeDetectorRef);

  private _message!: string;
  public set message(msg: string)
  {
    this._message = msg;
    this._cd.detectChanges();
  }

  public get message(): string
  {
    return this._message;
  }

  constructor() 
  {
    
  }
}
