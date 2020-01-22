import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Kh2po4Component } from './kh2po4.component';

describe('Kh2po4Component', () => {
  let component: Kh2po4Component;
  let fixture: ComponentFixture<Kh2po4Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Kh2po4Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Kh2po4Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
