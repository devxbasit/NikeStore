import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IframeComponent } from './components/iframe/iframe.component';
import { SafePipe } from './pipes/safe.pipe';

@NgModule({
  declarations: [IframeComponent, SafePipe],
  imports: [CommonModule],
  exports: [IframeComponent, SafePipe],
})
export class SharedModule {}
