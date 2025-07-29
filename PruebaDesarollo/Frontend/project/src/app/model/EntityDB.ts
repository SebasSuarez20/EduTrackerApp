export class EntityDB {
    protected companyCode: number;
    public username: string;
    protected enabled: boolean;

    constructor(){
        this.companyCode = -1;
        this.username = "";
        this.enabled = true;
    }
}