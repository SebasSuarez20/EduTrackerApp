export interface ICrud {
    getAllInformation():Promise<void>
    updateInformation():Promise<void>
    createInformation():Promise<void>
    deleteInformation():Promise<void>
}