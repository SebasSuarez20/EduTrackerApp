import { Injectable, signal } from '@angular/core';
import { IWebSocket } from '../../interface/websocket/IWebSocket';
import { BehaviorSubject} from 'rxjs';
import * as signalR from '@microsoft/signalr';




@Injectable({
  providedIn: "root",
})
export class SignalService implements IWebSocket {
  private hubConnection!: signalR.HubConnection;
  public listeningWebSocket = new BehaviorSubject<string | null>(null);

  constructor() {}

  public startConnection(Username: number, CompanyCode: number): Promise<void> {
    return new Promise((resolve) => {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("http://10.0.2.241:8080/BackEnd/SocketHub?userId=-1&companyCode=-1")
        .build();

      this.hubConnection
        .start()
        .then(() => {
          console.log("Conexión establecida");
          this.invokeUpdateInformation();
          this.joinGroupInvoke(Username, CompanyCode);
        })
        .catch((err: string) => console.error("Error al conectar:", err));

      resolve();
    });
  }

  public invokeUpdateInformation(): void {
    this.hubConnection.on("MessageTrackingWeb", (res: string | null) => {
      this.listeningWebSocket.next(res);
      console.log("Se activo correctamente su mama");
    });
  }

  public joinGroupInvoke(client: number, companyCode: number) {
    this.hubConnection
      .invoke("CreateFileUploadGroup", client, companyCode).then(()=>{
         console.log("Sucess: Se creo correctamente el grupo.");
      })
      .catch((err: string) => {
        console.error("No se pudo conectar", err);
      });
  }

  public getListeningWebSocket(callback:()=>void){

     this.listeningWebSocket.subscribe((res)=>{
      callback();
     })
  }
}
 