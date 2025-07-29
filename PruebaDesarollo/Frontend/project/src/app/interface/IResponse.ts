import { HttpStatusCode } from "@angular/common/http";


export interface IResponse<T>{
    dataContent:T | null,
    message:string,
    status: HttpStatusCode,
    isValid: boolean
}