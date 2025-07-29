import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ValidatorFieldsService {

  public isValidator:boolean = false;

  constructor() { }

   // Validacion de campos cuando no es aceptable
  public errorField(info:string | number | boolean | null | undefined):string{
      if(this.isValidator){
         if(info == "" || info == -1 || info == null || info == undefined)
         return "2px solid orange";
      }
      return "";
  }

}
