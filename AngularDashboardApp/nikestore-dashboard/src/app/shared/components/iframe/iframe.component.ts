import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-iframe',
  templateUrl: './iframe.component.html',
  styleUrls: ['./iframe.component.css'],
})
export class IframeComponent {
  @Input({ required: true }) src = '';
  @Input({ required: true }) heading = '';
  @Input({ required: true }) width = '';
  @Input({ required: true }) height = '';
}
