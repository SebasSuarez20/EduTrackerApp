import { TestBed } from '@angular/core/testing';

import { AuthLoggedService } from './auth-logged.service';

describe('AuthLoggedService', () => {
  let service: AuthLoggedService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthLoggedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
