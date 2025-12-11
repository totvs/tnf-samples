import { CommonModule } from "@angular/common";
import { booleanAttribute, Component, EventEmitter, inject, Input, Output, TemplateRef } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { PoBreadcrumbItem, PoBreadcrumbModule, PoButtonModule } from "@po-ui/ng-components";
import { Location } from '@angular/common';

@Component({
    selector: 'page-default',
    standalone: true,
    imports: [
        CommonModule,
        PoBreadcrumbModule,
        PoButtonModule,
    ],
    templateUrl: 'page-default.component.html',
    styleUrl: 'page-default.component.scss'
})
export class PageDefaultComponent {

    private router = inject(Router);
    private activatedRoute = inject(ActivatedRoute)
    private location = inject(Location);

    @Input() title: string = '';
    @Input({ transform: booleanAttribute }) enableStepper: boolean = false;
    @Input({ transform: booleanAttribute }) enableArrowAction: boolean = false;
    @Input({ transform: booleanAttribute }) enableContainerContent: boolean = false;
    @Input() breadcrumbItems: PoBreadcrumbItem[] = [];
    @Input({ required: true }) customTemplateRef!: TemplateRef<HTMLElement>;
    @Input({ required: false }) rightTemplateRef!: TemplateRef<HTMLElement>;

    @Output() arrowActionResponse: EventEmitter<any> = new EventEmitter();
    @Output() buttonActionResponse: EventEmitter<any> = new EventEmitter();

    protected onBack() {

        // console.log(this.activatedRoute.snapshot.url);
        this.location.back();
    }
}