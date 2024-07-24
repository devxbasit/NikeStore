import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SafePipe } from './pipes/safe.pipe';
import { IframeWrapperComponent } from './components/iframe-wrapper/iframe-wrapper.component';
import { NgxBootstrapIconsModule } from 'ngx-bootstrap-icons';
import { arrowUpRightSquare } from 'ngx-bootstrap-icons';

const icons = {
  arrowUpRightSquare,
};

@NgModule({
  declarations: [IframeWrapperComponent, SafePipe],
  imports: [CommonModule, NgxBootstrapIconsModule.pick(icons)],
  exports: [IframeWrapperComponent, SafePipe],
})
export class SharedModule {}
