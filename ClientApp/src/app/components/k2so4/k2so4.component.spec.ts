import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { K2so4Component } from './k2so4.component';

describe('K2so4Component', () => {
  let component: K2so4Component;
  let fixture: ComponentFixture<K2so4Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ K2so4Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(K2so4Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
