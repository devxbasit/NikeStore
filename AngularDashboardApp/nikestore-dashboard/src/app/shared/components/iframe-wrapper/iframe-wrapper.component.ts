import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  ViewChild,
} from '@angular/core';

@Component({
  selector: 'app-iframe-wrapper',
  templateUrl: './iframe-wrapper.component.html',
  styleUrls: ['./iframe-wrapper.component.css'],
})
export class IframeWrapperComponent implements AfterViewInit {
  @Input({ required: true }) src = '';
  @Input({ required: true }) title = '';
  @Input({ required: true }) height = '';
  @ViewChild('iframe') iFrame!: ElementRef<HTMLIFrameElement>;

  isLoading = true;

  ngAfterViewInit(): void {
    if (this.iFrame) {
      this.iFrame.nativeElement.onload = () => {
        this.isLoading = false;
      };
    }
  }
}
