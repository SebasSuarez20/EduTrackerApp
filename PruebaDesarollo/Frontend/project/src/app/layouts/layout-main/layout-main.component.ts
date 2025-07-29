import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SignalService } from '../../services/webSocket/signal.service';
import { EncryptionService } from '../../services/crypto/encryption.service';

@Component({
  selector: 'app-layout-main',
  standalone: true,
  imports: [CommonModule,RouterOutlet],
  templateUrl: './layout-main.component.html',
  styleUrl: './layout-main.component.css'
})
export class LayoutMainComponent implements OnInit {

  constructor(private webSocket: SignalService,private encrypt:EncryptionService){}


  ngOnInit(): void {

    const { companyCode, username } = JSON.parse(this.encrypt.decryptData(sessionStorage.getItem('$trkdta')?.toString() ?? ""));
    this.webSocket.startConnection(parseInt(username), parseInt(companyCode));
  }

}
