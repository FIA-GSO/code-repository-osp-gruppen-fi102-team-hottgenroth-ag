import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ProjectRouletteComponent } from '../../features/project-roulette/project-roulette.component';
import { MatCardModule } from '@angular/material/card';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-project-page',
  standalone: true,
  imports: [CommonModule, ProjectRouletteComponent, MatCardModule, MatButtonToggleModule, MatIconModule],
  templateUrl: './project-page.component.html',
  styleUrl: './project-page.component.scss'
})
export class ProjectPageComponent {
  sortedBy: 'date' | 'alphabet' = 'date';

  constructor(){}
  
  public toggleSorted(pSorting: 'date' | 'alphabet')
  {
    this.sortedBy = pSorting;
  }
}
