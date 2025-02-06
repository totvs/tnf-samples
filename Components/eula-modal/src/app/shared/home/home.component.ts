import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { PageDefaultComponent } from "@shared/components/page-default/page-default.component";

@Component({
    selector: 'home',
    standalone: true,
    imports: [
        CommonModule,
        PageDefaultComponent
    ],
    template: `

    <page-default 
        [customTemplateRef]="page" 
        [title]="''" />

    <ng-template #page>
        <div class="h-[500px] flex flex-col justify-center items-center mt-6">
            <p class="text-xl">
                Seja bem vindo ao <span class="font-semibold">Portal TOTVS Apps!</span>
            </p>    
            <div class="mt-6">
                <p>Navegue através do menu lateral para acessar a funcionalidade desejada</p>
            </div>
            <img class="mt-[300px]" src="https://manager.dev.totvs.app/assets/images/home-illustration.svg" />
        </div>
    </ng-template>
    `
})
export class HomeComponent {

}