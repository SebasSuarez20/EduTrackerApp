import { Component, EventEmitter, Input, input, OnDestroy, OnInit, Output, Renderer2, ViewChild } from '@angular/core';
import { MatDialogContent } from '@angular/material/dialog';
import { HttpRequestService } from '../../../services/http-request.service';
import { SubjectsassignationDTO, SubjectsassignationDTOSend } from '../../../model/SubjectsassignationDTO';
import { HttpStatusCode } from '@angular/common/http';
import Swal from 'sweetalert2';
import { AlertService } from '../../../services/alert/alert.service';
import { IResponse } from '../../../interface/IResponse';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { number } from 'echarts';


@Component({
  selector: 'app-insert-popup',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './insert-popup.component.html',
  styleUrl: './insert-popup.component.css'
})
export class InsertPopupComponent implements OnInit {

  @Output() close = new EventEmitter;

  public codeTeacher: { code: string, name: string, idx: number }[] = [];
  public codeTeacherAll: { code: string, name: string }[] = [];
  public listInformation: Partial<SubjectsassignationDTOSend>[] = [];
  public idxArray: number[] = [];
  public listInformationSend: { idFkStudent: number | null, idFkSubject: number | null, idControl: number | null, idFkSubjectAssignation: number }[] = [];
  public codeTeacherCopy: { code: string, name: string, idx: number, idxAssignation: number }[] = [];
  public formConsult!: FormGroup;
  public subjectAll: string[]= [];
  public isConsult:boolean = false;

  constructor(private readonly _serviceHttp: HttpRequestService, private readonly alertService: AlertService,private readonly reader:Renderer2,private fb:FormBuilder) {
    this.addInformationList();

        this.formConsult = this.fb.group({
        subjectStudent: new FormControl("")
      });
  }

  ngOnInit(): void {
    this.loadAsyncInformation();
    this.getAsyncSubjectAll();
  }

  public addInformationList(): void {

    this.listInformation.push({
      idFkStudent: null,
      idFkSubject: null,
      idControl: null,
      nameTeacher: "",
      name: "Primer Materia"
    });
  }

  public validatorTeacher(event: Event, idx: number) {

    const target = event.target as HTMLSelectElement;
    const value = target.value;

    if(!this.isConsult){
    var respMap = this.codeTeacherCopy.
      filter(s => s.idx === Number(value)).
      map(item => ({
        idFkStudent: null,
        idFkSubject: item.idx,
        idControl: null,
        idFkSubjectAssignation: item.idxAssignation
      }));

      console.log(respMap);
      console.log(this.codeTeacherCopy);

    this.listInformationSend = respMap;
    }else{

      const valuesFilter = this.codeTeacherCopy.filter(s=>s.idx === Number(value));

       this.listInformationSend.forEach((element,index)=>{
          element.idFkSubject = Number(value),
          element.idFkSubjectAssignation = valuesFilter[index].idxAssignation
      })

      console.log(this.listInformationSend);

    }

  }

  public closeModal() {
    this.close.emit();
  }

  public getAsyncSubjectAll(){
       this._serviceHttp.getHttp<Partial<SubjectsassignationDTOSend[]>>("/SubjectWithStudent/GetInformationUser").subscribe({
        next:(res)=>{

          if(res.status === HttpStatusCode.Ok){

            this.subjectAll = [];
            res.dataContent?.forEach((element)=>{
               this.subjectAll.push(element?.name ?? "");
            })

          }

        },
        error:(err)=>{
           console.error("error ",err);
           this.alertService.showError("Error: no se encontro informacion relacionada.",1700);
        }
       })
  }

  public getInformationForName(name:string){
      this.loadAsyncInformationAll(`/SubjectWithStudent/GetInformationForStudentAndAlls?name=${name}`);
  }

    public loadAsyncInformationAll(url: string): void {
    this._serviceHttp.getHttp<any>(url).subscribe({
      next: (res) => {

       console.log(res.dataContent.header);

       this.listInformationSend = res.dataContent.header.map((item: { idx: number; idxAssignation: number;idxSubjectStudent:number;idFkStudent:number }) => ({
        idFkStudent: item.idFkStudent,
        idFkSubject: item.idx,
        idControl: item.idxSubjectStudent,
        idFkSubjectAssignation: item.idxAssignation
      }));

      console.log(this.listInformationSend);

       this.isConsult = true;

      }, error: (err) => {
        console.error("Error loading information", err);
      }
    })
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

            this.codeTeacherCopy = response.dataContent?.map(item => ({
              code: item.name ?? "",
              name: item.nameTeacher ?? "",
              idx: item.idxSubject,
              idxAssignation: item.idxSubjectAssignation
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

    if (this.listInformationSend.length <= 0) {
      this.alertService.showError("Debe seleccionar un profesor para cada materia",1500);
      return;
    } else {

      this.alertService.showConfirmation("¿Estás seguro?", "Vas a guardar la información.").then((res) => {

        if (res) {
          this._serviceHttp.postHttp<Partial<SubjectsassignationDTOSend>[]>("/SubjectWithStudent/InsertInformationStudentWithSubject", this.listInformationSend).subscribe({
            next: (response) => {

              if (response.status === HttpStatusCode.Ok) {
  

                this.alertService.showSucess("Registro guardado correctamente.",1500).then(()=>{
                     this.formConsult.get('subjectStudent')?.setValue('');
                     this.getAsyncSubjectAll();
                     this.isConsult = false;
                })
              }

            }, error: (err) => {

               this.formConsult.get('subjectStudent')?.setValue('');
              console.error("error: ",err);
              this.alertService.showError("Error al guardar la información.",1500);
            }
          })
        }

      })
    }

  }

}
