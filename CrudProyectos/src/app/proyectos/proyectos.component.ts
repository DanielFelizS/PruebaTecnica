import { Component, inject, OnInit } from '@angular/core';
import { DataService } from '../data.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-proyectos',
  imports: [CommonModule, FormsModule],
  templateUrl: './proyectos.component.html',
  styleUrl: './proyectos.component.css'
})
export class ProyectosComponent implements OnInit {
  items: any[] = [];
  newItemName: string = '';
  newItemDescription: string = '';
  editItemId: number | null = null;
  editItemName: string = '';
  editItemDescription: string = '';

  private dataService = inject(DataService);

  ngOnInit() {
    this.loadItems();
  }

  loadItems() {
    this.dataService.getProyectos().subscribe((response) => {
      this.items = response;
    });
  }

  addItem() {
    const newItem = {
      nombre: this.newItemName,
      descripcion: this.newItemDescription,
    };
    
    this.dataService.createProyecto(newItem).subscribe(() => {
      this.loadItems();
      this.newItemName = '';
      this.newItemDescription = '';
    });
  }

    editItem(item: any) {
      this.editItemId = item.id;
      this.editItemName = item.nombre;
      this.editItemDescription = item.descripcion;
    }

    updateItem() {
      if (!this.editItemId) return;
      
      const updatedItem = {
        id: this.editItemId,
        nombre: this.editItemName,
        descripcion: this.editItemDescription
      };
      
      this.dataService.updateProyecto(this.editItemId, updatedItem).subscribe(() => {
        this.loadItems();
        this.cancelEdit();
      });
    }

    deleteItem(id: number) {
        this.dataService.deleteProyecto(id).subscribe(()=> {
          this.loadItems();
        });
      }

    cancelEdit() {
      this.editItemId = null;
      this.editItemName = '';
      this.editItemDescription = '';
    }
}
