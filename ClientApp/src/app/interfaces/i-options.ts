import { IColumnHeader } from "./i-column-header";

export interface IOptions {
    columnHeaders: IColumnHeader [],
    isRadOnly: boolean,
    rowsPerPage: number
}
