import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IframeWrapperComponent } from './iframe-wrapper.component';

describe('IframeWrapperComponent', () => {
  let component: IframeWrapperComponent;
  let fixture: ComponentFixture<IframeWrapperComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [IframeWrapperComponent],
    });
    fixture = TestBed.createComponent(IframeWrapperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
