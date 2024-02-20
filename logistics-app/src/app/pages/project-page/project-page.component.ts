import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ProjectRouletteComponent } from '../../features/project-roulette/project-roulette.component';

@Component({
  selector: 'app-project-page',
  standalone: true,
  imports: [CommonModule, ProjectRouletteComponent],
  templateUrl: './project-page.component.html',
  styleUrl: './project-page.component.scss'
})
export class ProjectPageComponent {

}
