import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NikeStoreAppComponent } from './nikestore-app.component';

describe('NikeStoreAppComponent', () => {
  let component: NikeStoreAppComponent;
  let fixture: ComponentFixture<NikeStoreAppComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NikeStoreAppComponent],
    });
    fixture = TestBed.createComponent(NikeStoreAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
