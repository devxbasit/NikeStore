import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-iframe-wrapper',
  templateUrl: './iframe-wrapper.component.html',
  styleUrls: ['./iframe-wrapper.component.css'],
})
export class IframeWrapperComponent {
  @Input({ required: true }) src = '';
  @Input({ required: true }) title = '';
  @Input({ required: true }) height = '';
}
