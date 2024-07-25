import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IEnvironment } from 'src/environments/ienvironment.interface';

@Component({
  selector: 'app-rabbit-mq-manager',
  templateUrl: './rabbit-mq-manager.component.html',
  styleUrls: ['./rabbit-mq-manager.component.css'],
})
export class RabbitMqManagerComponent {
  env: IEnvironment = environment;
  username: string = this.env.rabbitMqUsername;
  password: string = this.env.rabbitMqPassword;
  hiddenPassword: string = '****************';
}
