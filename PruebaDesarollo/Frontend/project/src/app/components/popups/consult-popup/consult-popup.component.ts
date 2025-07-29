import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { HttpRequestService } from '../../../services/http-request.service';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';
import { HttpStatusCode } from '@angular/common/http';
import Swal from 'sweetalert2';
import { AlertService } from '../../../services/alert/alert.service';

@Component({
  selector: 'app-consult-popup',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './consult-popup.component.html',
  styleUrl: './consult-popup.component.css'
})
export class ConsultPopupComponent implements OnInit, OnDestroy {


  @Output() close = new EventEmitter;
  public codeTeacher: { code: string, idx: number }[] = [];
  public formConsult!: FormGroup;
  public suscription$!: Subscription | undefined;
  public ListInformationSubject: Partial<any>[] = [];
  public ListInformationStudent: Partial<any>[] = [];
  public strCodeTeacher: string = "";

  constructor(private readonly _serviceHttp: HttpRequestService, private readonly fb: FormBuilder, private readonly alertService: AlertService) {
    this.formConsult = this.fb.group({
      codeTeacher: new FormControl("")
    });
  }

  ngOnInit(): void {

    this.suscription$ = this.formConsult.get("codeTeacher")?.valueChanges.subscribe((res) => {

      console.log(res);

      if (res !== "") {
        this.strCodeTeacher = res;
        this.loadAsyncInformationAll(`/SubjectWithStudent/GetInformationForStudentAndAlls?name=${res}`);
      } else {
        this.ListInformationStudent = [];
        this.ListInformationSubject = [];
      }

    });
    this.loadAsyncInformationAll("/SubjectWithStudent/GetInformationForStudentAndAlls");
  }

  ngOnDestroy(): void {
    if (this.suscription$) this.suscription$.unsubscribe();
  }


  public closeModal() {
    this.close.emit();
  }

  public loadAsyncInformationAll(url: string): void {
    this._serviceHttp.getHttp<any>(url).subscribe({
      next: (res) => {

        if (res.dataContent.header) {

          this.ListInformationSubject = res.dataContent.header;
          this.ListInformationStudent = res.dataContent.items;

          if (this.ListInformationSubject.length == 0) {
            this.ListInformationStudent = [];
            this.ListInformationSubject = [];
            this.formConsult.get("codeTeacher")?.setValue('');
            this.codeTeacher = [];
          }

        } else {
          this.codeTeacher = res.dataContent?.listSelect.filter((s: { idx: number; }) => s.idx == 1).map((item: { name: any; nameTeacher: any; idxSubjects: any; }) => ({
            code: item.name ?? "",
            idx: item.idxSubjects ?? 0
          })) ?? [];

          console.log(this.codeTeacher);
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
    if (this.suscription$) this.suscription$.unsubscribe();
  }

  public async deleteItem() {

    const { codeTeacher } = this.formConsult.value;

    if (codeTeacher != '') {

      this.alertService.showConfirmation("¿Estás seguro?", "¿Deseas eliminar el item correspondiente?").then((res) => {
        if (res) {

          const idx = this.codeTeacher.find(s => s.code === codeTeacher)?.idx

          this._serviceHttp.deleteHttp<number>(`/SubjectWithStudent/DeleteInformationStudentWithSubject?id=${idx}`).subscribe((res) => {

            if (res.status === HttpStatusCode.Ok) {
              this.loadAsyncInformationAll(`/SubjectWithStudent/GetInformationForStudentAndAlls?name=${this.strCodeTeacher}`);
            }

          });


        }
      });

    } else {
      this.alertService.showError("Error: por favor selecciona un profesor para consultar.", 1500, true);
    }
  }

}
