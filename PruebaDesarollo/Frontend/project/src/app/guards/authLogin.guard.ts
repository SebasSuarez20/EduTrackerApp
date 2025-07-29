import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthLoggedService } from '../services/auth-logged.service';

@Injectable({
  providedIn: 'root'
})
export class AuthLoginGuard {
  constructor(private router: Router,private loggedService:AuthLoggedService) {}

  canActivate(): boolean {
    if(!this.loggedService.validatorTokenLogged()){
       return true;
    }
    this.router.navigate(["/dashboard"]);
    return false;
  }
  
}