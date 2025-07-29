import { Component, Input, input, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { MatDialogContent } from '@angular/material/dialog';
import { HttpRequestService } from '../../../services/http-request.service';
import { SubjectsassignationDTO, SubjectsassignationDTOSend } from '../../../model/SubjectsassignationDTO';
import { HttpStatusCode } from '@angular/common/http';
import Swal from 'sweetalert2';
import { AlertService } from '../../../services/alert/alert.service';


@Component({
  selector: 'app-insert',
  standalone: true,
  imports: [MatDialogContent],
  templateUrl: './insert.component.html',
  styleUrl: './insert.component.css'
})
export class InsertComponent implements OnInit {


   public codeTeacher:{code:string,name:string,idx:number}[]=[];
   public listInformation:Partial<SubjectsassignationDTOSend>[] = [];
   public idxArray:number[] =[];

  constructor(private readonly _serviceHttp: HttpRequestService,private readonly alertService:AlertService) {



  }


  ngOnInit(): void {
    this.loadAsyncInformation();
  }

 
 

  public validatorTeacher(event:Event,idx:number){

      const target = event.target as HTMLSelectElement;
      const value = target.value;

       


  }

  public onModalClosed() {
  this.idxArray = [];
  this.listInformation = [];
}

  public loadAsyncInformation(): Promise<void> {
    return new Promise<void>((resolve, reject) => {

      this._serviceHttp.getHttp<SubjectsassignationDTO[]>("/SubjectWithStudent/GetAllInformationTeacher").subscribe({
        next: (response) => {

          if (response.status === HttpStatusCode.Ok) {

            this.codeTeacher = response.dataContent?.filter(s => s.idx == 1).map(item => ({
              code: item.name ?? "",
              name: item.nameTeacher ?? "",
              idx: item.idxSubject
            })) ?? [];

          }

          resolve();
        }, error: (err) => {
          console.error(err);
          reject(err);
        }
      })

    })
  }

  public saveInformationList(): void {

      const validatorFields = this.listInformation.filter(item => item.idFkSubject == null);

      if(validatorFields.length == 3){
        Swal.fire({
          icon: "error",
          text: "Debe seleccionar un profesor para cada materia.",
          timer: 1500,
          toast: true,
          showConfirmButton: false,
        });
        return;
      }else{
      
        console.log(this.listInformation);

        const result = [...this.listInformation];


        result.forEach((item) => {
          delete item.name;
          delete item.nameTeacher;
        });

        console.log(result);
        this._serviceHttp.postHttp<Partial<SubjectsassignationDTOSend>[]>("/SubjectWithStudent/InsertInformationStudentWithSubject",result.filter(s=>s.idFkSubject!=null)).subscribe({
           next:(responnse)=>{

               if(responnse.status === HttpStatusCode.Ok) {
                 Swal.fire({
                   icon: "success",
                   text: "Registro guardado correctamente.",
                   timer: 1500,
                   toast: true,
                   showConfirmButton: false,
                 }).then(() => {
                    
                 });
               }

           },error:(err)=>{
              Swal.fire({
                icon: "error",
                text: `Error al guardar la información.`,
                timer: 1500,
                toast: true,
                showConfirmButton: false,
              });
            }
        })

  }


  }

}
