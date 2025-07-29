import { Injectable } from '@angular/core';
import { ICrud } from '../../interface/crud/ICrud';
import { HttpRequestService } from '../http-request.service';
import { ShareInformationService } from '../share-information.service';

@Injectable({
  providedIn: 'root'
})
export class PaginatorService<T> implements ICrud{
  public countNumber:{isClic:boolean,data:number[][]}[] = [];
  public limitPage: number[] = [];
  public numberPage: number = 0;
  private readonly pageFinish =10;

  constructor(
      private service: HttpRequestService,
       private shareInfo: ShareInformationService<T[]>
  ) { }
  
  public getAllInformation(initial:number = 0, finish:number = this.pageFinish): Promise<void> {

       return new Promise((resolve) => {
          this.service.getHttp<T[]>(`private/doController/ConsultDo?initial=${initial}&finish=${finish}`).subscribe({
              next: (res) => {
                this.shareInfo.setInformation(res?.dataContent ?? []);
                this.shareInfo.paginator = res.paginator;
                
                resolve();
              },
              error: (err) => {
                console.log(err.message);
                resolve();
              },
            });
        });
  }


  public paginatorConsultPrevious() {

    if (this.limitPage.at(0) != 1) {
      this.limitPage = this.limitPage.map((i) => {
        return i - 1;
      });
    }
  }

  public paginatorConsultNext() {
    if (this.limitPage.at(-1) != this.numberPage) {
      this.limitPage = this.limitPage.map((i) => {
        return i + 1;
      });
    }
  }

  public paginatorConsultNumber(event: Event):Promise<void> {

    return new Promise(async (resolve, reject)=>{
      const target = event.target as HTMLElement;
      const id = Number(target.id);
  
      if (id != 0 && !this.countNumber[id].isClic) {
        this.countNumber[id].data[0][0] = this.countNumber[id].data[0][0] + 1;
        this.countNumber[id].data[0][1] = this.countNumber[id].data[0][1] + 1;
      }
      this.countNumber[id].isClic = true;
  
     await this.getAllInformation(this.countNumber[id].data[0][0], this.countNumber[id].data[0][1])
    resolve();

    })
  }

  public paginatorIterator() {

    let iterador = 1;
    this.limitPage = [];

    for (let index = 0; index < this.numberPage * this.pageFinish; index += this.pageFinish) {
      if (iterador <= 3) {
        this.limitPage.push(iterador);
      }
      this.countNumber.push({ data:[[index, iterador * this.pageFinish]],isClic:false });
      iterador++;
    }
  }

 public getlimitPage():number[]{
    return this.limitPage;
  }

  public setlimitPage(limit:number){
    this.limitPage.push(limit);
  }

  public getnumberPage():number{
    return this.numberPage;
  }

  public setnumberPage(num:number){
    this.numberPage = num;
  }

  updateInformation(): Promise<void> {
    throw new Error('Method not implemented.');
  }
  createInformation(): Promise<void> {
    throw new Error('Method not implemented.');
  }
  deleteInformation(): Promise<void> {
    throw new Error('Method not implemented.');
  }
}
