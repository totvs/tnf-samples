import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'tableAction',
    standalone: true
})
export class TableActionPipe implements PipeTransform {
    transform(value: Array<any>, ...args: any[]) {
        console.log(value);
        console.log(args);
    }

}