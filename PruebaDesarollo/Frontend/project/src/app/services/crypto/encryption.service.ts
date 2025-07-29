import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class EncryptionService {

  private readonly TOKEN_AES = '3T&}mw.A5G2!*<z!S#_2Aa3OdHRCjTUx';
  private readonly IV_AES = CryptoJS.enc.Utf8.parse("}8~*p5h'7RvL-Wr)"); 

  constructor() { }

  public encryptData(value: string): string {
    const key = CryptoJS.enc.Utf8.parse(this.TOKEN_AES);
    const encrypted = CryptoJS.AES.encrypt(value, key, {
      iv: this.IV_AES,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7
    });
    return encrypted.toString(); // Base64
  }

  public decryptData(value: string): string {
    const key = CryptoJS.enc.Utf8.parse(this.TOKEN_AES);
    const decrypted = CryptoJS.AES.decrypt(value, key, {
      iv: this.IV_AES,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7
    });
    return decrypted.toString(CryptoJS.enc.Utf8);
  }
}
