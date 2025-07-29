import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultPopupComponent } from './consult-popup.component';

describe('ConsultPopupComponent', () => {
  let component: ConsultPopupComponent;
  let fixture: ComponentFixture<ConsultPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConsultPopupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsultPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
