import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleBasedActionsComponent } from './role-based-actions.component';

describe('RoleBasedActionsComponent', () => {
  let component: RoleBasedActionsComponent;
  let fixture: ComponentFixture<RoleBasedActionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoleBasedActionsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoleBasedActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
