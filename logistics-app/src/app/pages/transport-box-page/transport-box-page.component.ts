import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { TransportBoxListComponent } from '../../features/transport-box-list/transport-box-list.component';
import { MatCardModule } from '@angular/material/card';
import {MatTabsModule} from '@angular/material/tabs';
import { TransportBoxDetailsComponent } from '../../features/transport-box-details/transport-box-details.component';

@Component({
  selector: 'app-transport-box-page',
  standalone: true,
  imports: [CommonModule, TransportBoxListComponent, MatCardModule, MatTabsModule, TransportBoxDetailsComponent],
  templateUrl: './transport-box-page.component.html',
  styleUrl: './transport-box-page.component.scss'
})
export class TransportBoxPageComponent {
  public selectedBox: string | undefined;

}
