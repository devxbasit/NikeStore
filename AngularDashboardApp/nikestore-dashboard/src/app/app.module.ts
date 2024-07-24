import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AboutComponent } from './components/about/about.component';
import { NotFound404Component } from './components/not-found-404/not-found-404.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { SharedModule } from './shared/shared.module';
import { CommonModule } from '@angular/common';
import { RabbitMqManagerComponent } from './components/rabbit-mq-manager/rabbit-mq-manager.component';
import { FlowDiagramComponent } from './components/flow-diagram/flow-diagram.component';
import { HangfireServeComponent } from './components/hangfire-serve/hangfire-serve.component';
import { WebApisComponent } from './components/web-apis/web-apis.component';
import { NikeStoreAppComponent } from './components/nikestore-app/nikestore-app.component';

@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    NotFound404Component,
    HeaderComponent,
    FooterComponent,
    RabbitMqManagerComponent,
    FlowDiagramComponent,
    HangfireServeComponent,
    WebApisComponent,
    NikeStoreAppComponent,
  ],
  imports: [BrowserModule, CommonModule, AppRoutingModule, SharedModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
