import { Component, Input } from '@angular/core';
import { Boat } from '../Models/boat';

@Component({
  selector: 'app-boat-card',
  templateUrl: './boat-card.component.html',
  styleUrl: './boat-card.component.css'
})
export class BoatCardComponent {
  @Input() boat!: Boat; //
}
