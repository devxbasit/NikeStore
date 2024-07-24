import { Component } from '@angular/core';
import { IIframeEndpoint } from 'src/app/interfaces/IIframeEndpoint.interface';
import { environment } from 'src/environments/environment';
import { IEnvironment } from 'src/environments/ienvironment.interface';

@Component({
  selector: 'app-hangfire-serve',
  templateUrl: './hangfire-serve.component.html',
  styleUrls: ['./hangfire-serve.component.css'],
})
export class HangfireServeComponent {
  env: IEnvironment = environment;

  swaggerEndpoint: IIframeEndpoint = {
    url: `${this.env.emailApiBaseUrl}/${this.env.hangfireServeRoutePath}`,
    title: 'Hangfire Server',
  };
}
