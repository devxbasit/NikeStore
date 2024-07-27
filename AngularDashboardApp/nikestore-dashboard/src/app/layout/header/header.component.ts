import { Component } from '@angular/core';
import { IconName } from 'ngx-bootstrap-icons';
import { environment } from 'src/environments/environment';
import { IEnvironment } from 'src/environments/ienvironment.interface';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent {
  env: IEnvironment = environment;

  navItems: { link: string; title: string; iconName: IconName }[] = [
    {
      link: '/app-flow-diagram',
      title: 'App Flow Diagram',
      iconName: 'diagram-3',
    },
    {
      link: '/nikestore-app',
      title: 'NikeStore App',
      iconName: 'shop',
    },
    {
      link: '/web-apis',
      title: 'Web APIs',
      iconName: 'tools',
    },
    {
      link: '/hangfire-server',
      title: 'Hangfire Server',
      iconName: 'stack',
    },
    {
      link: '/rabbit-mq-manager',
      title: 'RabbitMQ Manager',
      iconName: 'chat-right-text',
    },
    {
      link: '/about-the-project',
      title: 'About The Project',
      iconName: 'diagram-3',
    }
  ];
}
