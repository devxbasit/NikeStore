import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RabbitMqManagerComponent } from './rabbit-mq-manager.component';

describe('RabbitMqManagerComponent', () => {
  let component: RabbitMqManagerComponent;
  let fixture: ComponentFixture<RabbitMqManagerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RabbitMqManagerComponent]
    });
    fixture = TestBed.createComponent(RabbitMqManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
