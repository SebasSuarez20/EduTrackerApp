import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { AuthLoggedService } from '../services/auth-logged.service';
import { EncryptionService } from '../services/crypto/encryption.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {
  constructor(private router: Router,private loggedService:AuthLoggedService,private encrypt:EncryptionService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    console.log(state.url.toLocaleLowerCase());

    const URL = state.url.toLocaleLowerCase();
   

    if(!this.loggedService.validatorTokenLogged()){ 
      this.router.navigate([""]);
      return false;
    } else {

       const result = JSON.parse(this.encrypt.decryptData(sessionStorage.getItem('$trkdta')?.toString() ?? ""))

       if(URL.includes('/index/student') && parseInt(result.rol) === 2) {
          return true;
       }else if(URL.includes('/index/teacher') && parseInt(result.rol) === 1){
           return true;
       }else if(URL.includes('/index/administrator') && parseInt(result.rol) === 3){
           return true;
       }else{
           this.router.navigate(["/not-found"]);
          return false;
       }
    }
  }
  
}