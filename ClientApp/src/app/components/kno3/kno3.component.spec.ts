import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Kno3Component } from './kno3.component';

describe('Kno3Component', () => {
  let component: Kno3Component;
  let fixture: ComponentFixture<Kno3Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Kno3Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Kno3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
