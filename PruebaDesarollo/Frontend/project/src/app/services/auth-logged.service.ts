import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthLoggedService {

  constructor() { }

  public validatorTokenLogged(){
     var value = sessionStorage.getItem("token");
     return value ? true : false
  }

}
