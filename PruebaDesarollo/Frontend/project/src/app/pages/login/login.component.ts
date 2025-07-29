import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EncryptionService } from '../../services/crypto/encryption.service';
import { HttpRequestService } from '../../services/http-request.service';
import { HttpResponse, HttpStatusCode } from '@angular/common/http';
import Swal from 'sweetalert2';
import { ValidatorFieldsService } from '../../services/validator/validator-fields.service';
import { AlertService } from '../../services/alert/alert.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  public formLogin!: FormGroup;

  constructor(private router: Router, private encryption: EncryptionService,
     private fb: FormBuilder, private service: HttpRequestService, public validator: ValidatorFieldsService) {

    this.formLogin = this.fb.group({
       Identification: ["", Validators.required],
       Password: ["", Validators.required],
    })
  }

  public submit() {

    if (this.formLogin.valid) {

      console.log(encodeURIComponent(this.encryption.encryptData(JSON.stringify(this.formLogin.value))));
      console.log(this.formLogin.value);
       
      this.service.getHttp<string>(`/User/LoggedWithUser?data=${encodeURIComponent(this.encryption.encryptData(JSON.stringify(this.formLogin.value)))}`).subscribe({
        next: (res) => {
          if (res.status === HttpStatusCode.Ok) {
            Swal.fire({
              icon: "success",
              text: res.message,
              timer: 1500,
              toast: true,
              showConfirmButton: false,
            }).then(() => {
              sessionStorage.setItem("token", JSON.parse(this.encryption.decryptData(res?.dataContent ?? "")).token ?? "")
              sessionStorage.setItem("$trkdta", res?.dataContent ?? "");

              if(JSON.parse(this.encryption.decryptData(res?.dataContent ?? "")).darkMode ?? ""){
                this.changeDarkMode();
              }

              this.validator.isValidator = false;

              switch(JSON.parse(this.encryption.decryptData(res?.dataContent ?? "")).rol) {
                case "1":
                  this.router.navigateByUrl("/index/Teacher");
                  break;
                    case "2":
                  this.router.navigateByUrl("/index/Student");
                  break;
                    case "3":
                  this.router.navigateByUrl("/index/Administrator");
                  break;   
              }

            })
          }
        }, error: () => {

          Swal.fire({
            icon: "error",
            text: "Error: no se encontro informacion relacionada con el usuario.",
            timer: 2000,
            toast: true,
            showConfirmButton: false,
          })
        }
      })
    } else {

      Swal.fire({
        icon: "error",
        title: 'Campos incompletos',
        text: 'Por favor, completa todos los campos requeridos.',
        timer: 2000,
        toast: true,
        showConfirmButton: false,
      })
      this.validator.isValidator = true;
    }
  }

  public changeDarkMode(isClick:boolean = true): Promise<void> {

    return new Promise((resolve, reject) => {
      try {

        const body = document.body;
        body.classList.toggle("dark-mode");
  
        resolve();
      } catch (err) {
        reject(err);
      }
    })
  }

}
