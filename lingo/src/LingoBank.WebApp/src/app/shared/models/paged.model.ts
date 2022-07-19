
export class Paged<T> {
	public data: T[];
	public total: number;
	public pageNumber: number;
	public nextPage: number;
	public prevPage: number;
}
