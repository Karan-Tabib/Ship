import { Component, inject, OnInit } from '@angular/core';
import { BoatService } from './services/boat.service';
import { FishService } from './services/fish.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'ShipApp';
  boatService = inject(BoatService);
  fishService = inject(FishService);

  
  /**
   *
   */
  constructor() {

  }
  ngOnInit(): void {
    //get all the boats
    this.boatService.loadBoats();

    //load all fish
    this.fishService.LoadFish();
  }
}
