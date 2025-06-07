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

    const nombreRepetido = this.items.some(item => item.nombre.trim().toLowerCase() === this.newItemName.trim().toLowerCase());

    if (nombreRepetido) {
      alert('Ya existe un proyecto con ese nombre.');
      return;
    }
    
    this.dataService.createProyecto(newItem).subscribe(() => {
      this.loadItems();
      this.newItemName = '';
      this.newItemDescription = '';
    });
    alert("Se ha creado el proyecto correctamete");
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
    alert("Se ha editado el proyecto correctamete");

    }

    deleteItem(id: number) {
        this.dataService.deleteProyecto(id).subscribe(()=> {
        alert("Se ha eliminado el proyecto correctamete");

          this.loadItems();
        });
      }

    cancelEdit() {
      this.editItemId = null;
      this.editItemName = '';
      this.editItemDescription = '';
    }
}
