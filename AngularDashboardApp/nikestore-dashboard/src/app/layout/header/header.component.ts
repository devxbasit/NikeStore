import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IEnvironment } from 'src/environments/ienvironment.interface';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  env: IEnvironment = environment;
}
