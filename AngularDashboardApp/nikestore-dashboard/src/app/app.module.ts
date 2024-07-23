import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AboutComponent } from './components/about/about.component';
import { NotFound404Component } from './components/not-found-404/not-found-404.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { SharedModule } from './shared/shared.module';
import { CommonModule } from '@angular/common';
import { RabbitMqManagerComponent } from './components/rabbit-mq-manager/rabbit-mq-manager.component';
import { FlowDiagramComponent } from './components/flow-diagram/flow-diagram.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    AboutComponent,
    NotFound404Component,
    HeaderComponent,
    FooterComponent,
    RabbitMqManagerComponent,
    FlowDiagramComponent,
  ],
  imports: [BrowserModule, CommonModule, AppRoutingModule, SharedModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
