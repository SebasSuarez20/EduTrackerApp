import { AfterContentInit, Component, OnInit } from '@angular/core';
 import { MatCardModule } from '@angular/material/card';
  import { MatToolbarModule } from '@angular/material/toolbar';
import { HeaderComponent } from '../header/header.component';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { InsertComponent } from '../crudDialog/insert/insert.component';
import { ConsultComponent } from "../crudDialog/consult/consult.component";
import { HttpRequestService } from '../../services/http-request.service';
import { HttpStatusCode } from '@angular/common/http';
import Swal from 'sweetalert2';
import { BehaviorSubject, Observable } from 'rxjs';
import { InsertPopupComponent } from "../popups/insert-popup/insert-popup.component";
import { ConsultPopupComponent } from "../popups/consult-popup/consult-popup.component";

@Component({
  selector: 'app-role-based-actions',
  standalone: true,
  imports: [MatCardModule, MatToolbarModule, MatDialogModule, InsertComponent, ConsultComponent, ConsultComponent, HeaderComponent, InsertPopupComponent, ConsultPopupComponent],
  templateUrl: './role-based-actions.component.html',
  styleUrl: './role-based-actions.component.css'
})
export class RoleBasedActionsComponent implements AfterContentInit {

   public isInsert:boolean = false;
   public strlPoup: string = "";

   constructor(private readonly dialog: MatDialog,private serviceHttp:HttpRequestService){}


   ngAfterContentInit(): void {
       this.verificationSubject();
   }

   public openModal(modal:string):void{
      this.strlPoup = modal;
   }


   public messageVerifySubject():void{
        if(!this.isInsert){
         Swal.fire({
            text: 'Ya ha registrado las 3 materias correspondientes.',
            icon: 'warning',
            timer:1500,
            timerProgressBar:true,
            toast:true,
            showConfirmButton:false
         })
        }
   }
  

   public verificationSubject(): void {

    
     this.serviceHttp.getHttp<number>("/SubjectWithStudent/VerificationSubjectForUser").subscribe({
       next:(res)=>{

          if(res.status === HttpStatusCode.Ok){

             if(res.dataContent === 3){
                this.isInsert = false;
             }else{
               this.isInsert = true;
             }
             
          }
         
       },
       error:(err)=>{
          console.error(err)
       }
     })

   }

   public closeModal(): void {
      this.strlPoup = "";
   }


}
