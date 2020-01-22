import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Mgso4Component } from './mgso4.component';

describe('Mgso4Component', () => {
  let component: Mgso4Component;
  let fixture: ComponentFixture<Mgso4Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Mgso4Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Mgso4Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
