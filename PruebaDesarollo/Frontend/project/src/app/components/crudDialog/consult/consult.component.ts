import { Component, OnInit } from '@angular/core';
import { HttpRequestService } from '../../../services/http-request.service';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-consult',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './consult.component.html',
  styleUrl: './consult.component.css'
})
export class ConsultComponent implements OnInit {

  public codeTeacher:{code:string,idx:number}[]=[];
  public formConsult!:FormGroup;
  public suscription$!:Subscription | undefined;


  public ListInformationSubject:Partial<any>[] = [];
  public ListInformationStudent:Partial<any>[] = [];

  constructor(private readonly _serviceHttp: HttpRequestService,private fb:FormBuilder) {
      this.formConsult = this.fb.group({
        codeTeacher: new FormControl("")
      });
  }


  ngOnInit(): void {

     this.suscription$ =  this.formConsult.get("codeTeacher")?.valueChanges.subscribe((res)=>{

       if(res !== ""){
         this.loadAsyncInformationAll(`/SubjectWithStudent/GetInformationForStudentAndAlls?name=${res}`);
       }
       
     });
     this.loadAsyncInformationAll("/SubjectWithStudent/GetInformationForStudentAndAlls");
  }

  public loadAsyncInformationAll(url: string): void {
    this._serviceHttp.getHttp<any>(url).subscribe({
      next: (res) => {

        if (res.dataContent.header) {

          this.ListInformationSubject = res.dataContent.header;
          this.ListInformationStudent = res.dataContent.items;

        } else {
          this.codeTeacher = res.dataContent?.listSelect.filter((s: { idx: number; }) => s.idx == 1).map((item: { name: any; nameTeacher: any; idxSubjects: any; }) => ({
            code: item.name ?? "",
            idx: item.idxSubjects ?? 0
          })) ?? [];
        }

      }, error: (err) => {
        console.error("Error loading information", err);
      }
    })
  }

    public onModalClosed() {

       this.formConsult.get("codeTeacher")?.setValue("");
       this.ListInformationStudent = [];
       this.ListInformationSubject = [];
       if( this.suscription$) this.suscription$.unsubscribe();
}


}
