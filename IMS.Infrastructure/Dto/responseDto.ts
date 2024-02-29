export interface ResponseProblemDto {
    type: string | null;
    title: string | null;
    details: string | null;
    status: number;
}