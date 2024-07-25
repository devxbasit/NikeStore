import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './components/about/about.component';
import { NotFound404Component } from './components/not-found-404/not-found-404.component';
import { RabbitMqManagerComponent } from './components/rabbit-mq-manager/rabbit-mq-manager.component';
import { FlowDiagramComponent } from './components/flow-diagram/flow-diagram.component';
import { HangfireServeComponent } from './components/hangfire-serve/hangfire-serve.component';
import { WebApisComponent } from './components/web-apis/web-apis.component';
import { NikeStoreAppComponent } from './components/nikestore-app/nikestore-app.component';

const routes: Routes = [
  { path: '', redirectTo: '/app-flow-diagram', pathMatch: 'full' },
  { path: 'app-flow-diagram', component: FlowDiagramComponent },
  { path: 'nikestore-app', component: NikeStoreAppComponent },
  { path: 'web-apis', component: WebApisComponent },
  { path: 'hangfire-server', component: HangfireServeComponent },
  { path: 'rabbit-mq-manager', component: RabbitMqManagerComponent },
  { path: 'about-the-project', component: AboutComponent },
  { path: '**', component: NotFound404Component },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
