import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlowDiagramComponent } from './flow-diagram.component';

describe('FlowDiagramComponent', () => {
  let component: FlowDiagramComponent;
  let fixture: ComponentFixture<FlowDiagramComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FlowDiagramComponent]
    });
    fixture = TestBed.createComponent(FlowDiagramComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
