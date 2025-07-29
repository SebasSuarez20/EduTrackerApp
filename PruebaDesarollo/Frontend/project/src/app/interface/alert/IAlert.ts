export interface IAlert{
    
    showSucess( text: string, timer: number, isProgress: boolean):Promise<void>;
    showError( text: string, timer: number, isProgress: boolean):Promise<void>;
    showWarning(title: string, text: string, timer: number, isProgress: boolean):Promise<void>;
    showConfirmation(title:string,text:string,timer:number):Promise<boolean>;
    showInfo(title: string, text: string, timer: number, isProgress: boolean):Promise<void>;
    
}