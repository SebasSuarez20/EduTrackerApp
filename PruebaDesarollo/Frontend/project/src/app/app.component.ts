import { Component, computed, effect, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { IdleService } from './services/inactivitySystem/idle.service';
import Swal from 'sweetalert2';
// import { ShareInformationService } from './services/share-information.service';

@Component({
  selector: "app-root",
  standalone: true,
  imports: [CommonModule, RouterOutlet],
  styles: [
    `
      .errorField {
        border: 2px solid orange;
      }
    `,
  ],
  template: ` <router-outlet /> `,
})
export class AppComponent implements OnInit {
  private title = "Tracking web";

  // constructor(private sharedInformation: ShareInformationService<boolean>) {}

  ngOnInit(): void {
    // this.sharedInformation.getDataObservable().subscribe((res) => {
    //   if (res) {
    //     Swal.fire({
    //       title: "Inactividad detectada",
    //       text: "Por seguridad, tu sesión se cerrará en breve debido a inactividad.",
    //       icon: "warning",
    //       allowOutsideClick: false,
    //       allowEscapeKey: false,
    //       allowEnterKey: false,
    //       showConfirmButton: true,
    //     }).then(() => {
    //       this.sharedInformation.setDataObservable(null);
    //     });
    //   }
    // });
  }
}