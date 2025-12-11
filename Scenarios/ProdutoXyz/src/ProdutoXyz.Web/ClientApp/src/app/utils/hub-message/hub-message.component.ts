import { Component, OnInit, ViewChild, EventEmitter } from '@angular/core';
import { ThfModalComponent } from '@totvs/thf-ui/components/thf-modal/thf-modal.component';
import { ThfModalAction } from '@totvs/thf-ui/components/thf-modal';
import { Observable } from 'rxjs/Observable';
import { ObservableInput } from 'rxjs/Observable';
import { retry } from 'rxjs/operator/retry';

@Component({
    selector: 'app-hub-message',
    templateUrl: './hub-message.component.html',
    styleUrls: ['./hub-message.component.css']
})
/**
 * @description Componente de mensagens.
 * Pode ser utilizado como instância única, desde que declarado no app.component.
 */
export class HubMessageComponent {

    /**
     * Referência do componente thf-modal
     */
    @ViewChild(ThfModalComponent) modal: ThfModalComponent;

    /**
    * Dados utilizados pelo formulário
    */
    data = {
        primaryAction: this.getPrimaryAction('Primário'),
        secondaryAction: this.getSecondaryAction('Secundário'),
        title: '',
        body: '',
        typeMessage: ''
    };

    /**
     * Dispara o evento quando o confirmar ou cancelar do modal for clicado.
     */
    modalEvent: EventEmitter<boolean>;
    /**
     * Opções disponíveis para o checkbox
     */
    checkOpts: Array<{ disabled?: boolean, label: string, value: string }> = [];
    /**
     * Valores selecionados do checkbox
     */
    checkValues: Array<string> = [];
    /**
     * Referência do array enviado para criação do checkbox
     */
    inputCheck: Array<{ label: string, checked?: boolean }> = [];
    /**
     * Componente de mensagens.
     * Pode ser utilizado como instância única, desde que declarado no app.component.
     */
    constructor() { }
    /**
     * Monta e retorna a ação primária
     * @param label Label da ação primária
     */
    getPrimaryAction(label): ThfModalAction {
        return {
            action: () => { this.emitPrimary() },
            label: label
        };
    }
    /**
     * Monta e retorna a ação secundária
     * @param label Label da ação secundária
     */
    getSecondaryAction(label): ThfModalAction {
        return {
            action: () => { this.emitSecondary() },
            label: label
        };
    }

    /**
     * Apresenta a mensagem. Utilizado para mensagens de erro.
     * @param message mensagem
     * @param title título da mensagem, padrão 'Erro'
     */
    openMessageError(message, title = 'Erro') {
        this.data.typeMessage = 'error';
        this.data.primaryAction = this.getPrimaryAction('Ok');
        this.data.secondaryAction = undefined;
        this.data.title = title;
        this.data.body = message;
        this.modal.open();
    }

    /**
     * Apresenta a mensagem. Utilizado para mensagens informativas.
     * @param message mensagem
     * @param title título da mensagem, padrão 'Informação'
     */
    openMessageInformation(message, title = 'Informação') {
        this.data.typeMessage = 'information';
        this.data.primaryAction = this.getPrimaryAction('Ok');
        this.data.secondaryAction = undefined;
        this.data.title = title;
        this.data.body = message;
        this.modal.open();
    }

    /**
     * Apresenta a mensagem. Utilizado para mensagens de alerta.
     * @param message mensagem
     * @param title título da mensagem, padrão 'Atenção'
     */
    openMessageWarning(message, title = 'Atenção') {
        this.data.typeMessage = 'warning';
        this.data.primaryAction = this.getPrimaryAction('Ok');
        this.data.secondaryAction = undefined;
        this.data.title = title;
        this.data.body = message;
        this.modal.open();
    }

    /**
     * Realiza uma pergunta ao usuário.
     * @param message mensagem
     * @param title título da mensagem
     * @returns EventEmitter<boolean>
     * @example
     * .openMessageQuestion(message, title).subscribe((confirm) => {console.log(confirm)})
     */
    openMessageQuestion(message, title, options: Array<{ label: string, checked?: boolean }> = []): EventEmitter<boolean> {
        this.data.typeMessage = 'question';
        this.data.primaryAction = this.getPrimaryAction('Sim');
        this.data.secondaryAction = this.getSecondaryAction('Não');
        this.data.title = title;
        this.data.body = message;
        this.inputCheck = options;
        for (const opt of options) {
            this.checkOpts.push({ label: opt.label, value: opt.label });
            if (opt.checked) {
                this.checkValues.push(opt.label);
            }
        }
        this.modal.open();
        this.modalEvent = new EventEmitter();
        return this.modalEvent;
    }

    /**
    * Função disparada quando a ação principal for clicada pelo usuário
    */
    emitPrimary() {
        this.inputCheck.forEach((opt) => {
            if (this.checkValues.findIndex((value) => { return opt.label === value }) > -1) {
                opt.checked = true;
            } else {
                opt.checked = false;
            }
        });
        if (this.modalEvent && this.data.typeMessage === 'question') {
            this.modalEvent.emit(true);
            this.modalEvent.unsubscribe();
        }
        this.modal.close();
        this.checkOpts = [];
        this.inputCheck = [];
        this.checkValues = [];
    }

    /**
    * Função disparada quando a ação secundária for clicada pelo usuário
    */
    emitSecondary() {
        this.inputCheck.forEach((opt) => {
            if (this.checkValues.findIndex((value) => { return opt.label === value }) > -1) {
                opt.checked = true;
            } else {
                opt.checked = false;
            }
        });
        if (this.modalEvent && this.data.typeMessage === 'question') {
            this.modalEvent.emit(false);
            this.modalEvent.unsubscribe();
        }
        this.modal.close();
        this.checkOpts = [];
        this.inputCheck = [];
        this.checkValues = [];
    }
}
