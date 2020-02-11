import { IColumn } from "./i-column";

export interface IOptions {
    columns: IColumn [],
    isRadOnly: boolean,
    rowsPerPage: number
}
