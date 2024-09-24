import { Component } from '@angular/core';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'dental-appointment-frontend';

  public getCurrentVersion(): string {
    return environment.version;
  }

  public getCurrentYear(): number {
    return new Date().getFullYear();
  }
}
