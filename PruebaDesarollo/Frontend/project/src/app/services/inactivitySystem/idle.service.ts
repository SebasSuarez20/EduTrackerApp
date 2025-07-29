import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class IdleService {

  private timeoutId!:ReturnType<typeof setTimeout>;
  private readonly idleTime = 60 * 60 * 1000;
  private onTimeoutCallback: () => void = () => {};

  constructor() { }

  public startTime(callback:()=>void){
    this.onTimeoutCallback = callback;
    this.resetTime();
  }

  public resetTime():void{
    this.rebootTime();
    try{
      this.timeoutId = setTimeout(() => {
        this.onTimeoutCallback();
     }, this.idleTime);
    }catch(err){
       console.error("Error: (resetTime) ",err)
    }
  }

  public rebootTime(){
    clearTimeout(this.timeoutId);
  }

}
