import { Injectable } from '@angular/core';
import { IAlert } from '../../interface/alert/IAlert';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AlertService implements IAlert {

  constructor() { }


  public async showSucess(text: string, timer: number = 0, isProgress: boolean = false): Promise<void> {

    await Swal.fire({
      icon: 'success',
      text: text,
      allowOutsideClick:false,
      allowEscapeKey:false,
      showCancelButton: false,
      showConfirmButton: false,
      toast:true,
      timer: timer,
      timerProgressBar: isProgress
    })

  }

  public async showError(text: string, timer: number = 0, isProgress: boolean = false): Promise<void> {

    await Swal.fire({
      icon: 'error',
      text: text,
      allowOutsideClick: false,
      allowEscapeKey: false,
      showCancelButton: false,
      showConfirmButton: false,
      toast:true,
      timer: timer,
      timerProgressBar: isProgress
    })
  }

  public async showWarning(title: string, text: string, timer: number, isProgress: boolean = false): Promise<void> {

    await Swal.fire({
      icon: 'error',
      title: title,
      text: text,
      allowOutsideClick: false,
      allowEscapeKey: false,
      showCancelButton: false,
      showConfirmButton: false,
       toast:true,
      timer: timer,
      timerProgressBar: isProgress
    })

  }

  public async showConfirmation(title: string, text: string, timer: number = 0): Promise<boolean> {

    const confirm = await Swal.fire({
      title: title,
      text: text,
      allowOutsideClick: false,
      allowEscapeKey: false,
      showCancelButton: true,
       toast:true,
      confirmButtonText: "Confirmar",
    });

    return confirm?.isConfirmed ?? false;
  }

  public async showInfo(title: string, text: string, timer: number, isProgress: boolean = false): Promise<void> {
    await Swal.fire({
      icon: 'error',
      title: title,
      text: text,
        allowOutsideClick:false,
      allowEscapeKey:false,
      showCancelButton: false,
      showConfirmButton: false,
      timer: timer,
       toast:true,
      timerProgressBar: isProgress
    })
  }

}
