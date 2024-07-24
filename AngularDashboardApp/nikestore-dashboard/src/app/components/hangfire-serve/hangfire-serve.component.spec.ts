import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HangfireServeComponent } from './hangfire-serve.component';

describe('HangfireServeComponent', () => {
  let component: HangfireServeComponent;
  let fixture: ComponentFixture<HangfireServeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HangfireServeComponent]
    });
    fixture = TestBed.createComponent(HangfireServeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
