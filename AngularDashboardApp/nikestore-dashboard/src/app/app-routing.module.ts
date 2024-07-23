import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AboutComponent } from './components/about/about.component';
import { NotFound404Component } from './components/not-found-404/not-found-404.component';
import { RabbitMqManagerComponent } from './components/rabbit-mq-manager/rabbit-mq-manager.component';
import { FlowDiagramComponent } from './components/flow-diagram/flow-diagram.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'diagram', component: FlowDiagramComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'about', component: AboutComponent },
  { path: 'rabbit', component: RabbitMqManagerComponent },
  { path: '**', component: NotFound404Component },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
