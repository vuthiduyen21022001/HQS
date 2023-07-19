import { Component, OnInit } from '@angular/core';
import { InventoryService } from '../services/inventory.service';

@Component({
  selector: 'lib-inventory',
  template: ` <p>inventory works!</p> `,
  styles: [],
})
export class InventoryComponent implements OnInit {
  constructor(private service: InventoryService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
