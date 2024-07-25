import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IEnvironment } from 'src/environments/ienvironment.interface';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {

  env: IEnvironment = environment

}
