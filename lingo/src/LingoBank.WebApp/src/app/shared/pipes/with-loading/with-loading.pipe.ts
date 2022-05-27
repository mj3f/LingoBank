import { Pipe, PipeTransform } from '@angular/core';
import { isObservable, of } from 'rxjs';
import { catchError, map, startWith } from 'rxjs/operators';

@Pipe({
	name: 'withLoading'
})
export class WithLoadingPipe implements PipeTransform {

	transform(value: unknown): any {
		return isObservable(value)
			? value.pipe(
				map(v => ({ loading: false, v })),
				startWith({ loading: true }),
				catchError(error => of({ loading: true, error }))
			) :
			value;
	}

}
