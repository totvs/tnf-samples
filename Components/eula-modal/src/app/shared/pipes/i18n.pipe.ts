import { Pipe, PipeTransform } from '@angular/core';
import { PoI18nService } from '@po-ui/ng-components';

@Pipe({
    name: 'i18n',
    standalone: true,
})
export class I18nPipe implements PipeTransform {
    private literals: any = {};

    constructor(poI18nService: PoI18nService) {
        poI18nService
            .getLiterals()
            .subscribe((literals) => {
                this.literals = literals
            })
    }

    transform(literal: string, ...args: (string | number)[]): string {
        return this.interpolate(this.literals[literal] || literal, args);
    }

    private interpolate(literal: string, args: (string | number)[]): string {
        args.forEach(
            arg =>
            (literal = literal.replace(
                /(\{\w*\})+/,
                typeof arg === 'string' ? arg : arg.toString()
            ))
        );
        return literal;
    }
}
