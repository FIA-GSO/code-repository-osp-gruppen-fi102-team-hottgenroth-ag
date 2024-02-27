import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'transport-box-details',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatFormFieldModule, MatSelectModule, MatInputModule],
  templateUrl: './transport-box-details.component.html',
  styleUrl: './transport-box-details.component.scss'
})
export class TransportBoxDetailsComponent {
  @Input() selectedBox: any;
}
