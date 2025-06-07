import { Component } from '@angular/core';
import { ProyectosComponent } from './proyectos/proyectos.component';

@Component({
  selector: 'app-root',
  imports: [ProyectosComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'CrudProyectos';

}
