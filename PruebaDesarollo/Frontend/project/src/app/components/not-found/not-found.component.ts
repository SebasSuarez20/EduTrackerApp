import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthLoggedService } from '../../services/auth-logged.service';

@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [],
  templateUrl: './not-found.component.html',
  styleUrl: './not-found.component.css'
})
export class NotFoundComponent {

  constructor(private router:Router,private serviceLogged:AuthLoggedService){
      
    //  if(this.serviceLogged.validatorTokenLogged()) this.router.navigate(["/index/dashboard"]);
    //  else this.router.navigate([""]);
  }
}
