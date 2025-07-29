export class SubjectsassignationDTO{

    public name!:string | null;
    public nameTeacher!:string | null;
    public day!:number | null;
    public hoursInitial!:string | null;
    public hoursFinal!:string | null;
    public enabled!:boolean;
    public idx!:number;
    public idxSubject!:number;
    public idxSubjectAssignation !:number;
}

export class SubjectsassignationDTOSend{
     
     public idFkStudent!:number | null;
     public idFkSubject!:number | null;
     public idFkSubjectAssignation!:number | null;
     
     public idControl!:number | null;
     public name!:string;
     public nameTeacher!:string;
}