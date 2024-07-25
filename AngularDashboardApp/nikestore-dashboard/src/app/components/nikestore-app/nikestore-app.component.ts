import { Component } from '@angular/core';
import { IIframeEndpoint } from 'src/app/interfaces/IIframeEndpoint.interface';
import { environment } from 'src/environments/environment';
import { IEnvironment } from 'src/environments/ienvironment.interface';

@Component({
  selector: 'app-nikestore-app',
  templateUrl: './nikestore-app.component.html',
  styleUrls: ['./nikestore-app.component.css'],
})
export class NikeStoreAppComponent {
  env: IEnvironment = environment;
  nikeStoreAppLoginUrl = `${this.env.nikeStoreWebAppBaseUrl}`;
}
