import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WebApisComponent } from './web-apis.component';

describe('WebApisComponent', () => {
  let component: WebApisComponent;
  let fixture: ComponentFixture<WebApisComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WebApisComponent]
    });
    fixture = TestBed.createComponent(WebApisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
