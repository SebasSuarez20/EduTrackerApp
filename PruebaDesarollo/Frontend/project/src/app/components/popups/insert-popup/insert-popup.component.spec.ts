import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsertPopupComponent } from './insert-popup.component';

describe('InsertPopupComponent', () => {
  let component: InsertPopupComponent;
  let fixture: ComponentFixture<InsertPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InsertPopupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InsertPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
