"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var thf_modal_component_1 = require("@totvs/thf-ui/components/thf-modal/thf-modal.component");
var HubMessageComponent = /** @class */ (function () {
    /**
     * Componente de mensagens.
     * Pode ser utilizado como instância única, desde que declarado no app.component.
     */
    function HubMessageComponent() {
        /**
        * Dados utilizados pelo formulário
        */
        this.data = {
            primaryAction: this.getPrimaryAction('Primário'),
            secondaryAction: this.getSecondaryAction('Secundário'),
            title: '',
            body: '',
            typeMessage: ''
        };
        /**
         * Opções disponíveis para o checkbox
         */
        this.checkOpts = [];
        /**
         * Valores selecionados do checkbox
         */
        this.checkValues = [];
        /**
         * Referência do array enviado para criação do checkbox
         */
        this.inputCheck = [];
    }
    /**
     * Monta e retorna a ação primária
     * @param label Label da ação primária
     */
    HubMessageComponent.prototype.getPrimaryAction = function (label) {
        var _this = this;
        return {
            action: function () { _this.emitPrimary(); },
            label: label
        };
    };
    /**
     * Monta e retorna a ação secundária
     * @param label Label da ação secundária
     */
    HubMessageComponent.prototype.getSecondaryAction = function (label) {
        var _this = this;
        return {
            action: function () { _this.emitSecondary(); },
            label: label
        };
    };
    /**
     * Apresenta a mensagem. Utilizado para mensagens de erro.
     * @param message mensagem
     * @param title título da mensagem, padrão 'Erro'
     */
    HubMessageComponent.prototype.openMessageError = function (message, title) {
        if (title === void 0) { title = 'Erro'; }
        this.data.typeMessage = 'error';
        this.data.primaryAction = this.getPrimaryAction('Ok');
        this.data.secondaryAction = undefined;
        this.data.title = title;
        this.data.body = message;
        this.modal.open();
    };
    /**
     * Apresenta a mensagem. Utilizado para mensagens informativas.
     * @param message mensagem
     * @param title título da mensagem, padrão 'Informação'
     */
    HubMessageComponent.prototype.openMessageInformation = function (message, title) {
        if (title === void 0) { title = 'Informação'; }
        this.data.typeMessage = 'information';
        this.data.primaryAction = this.getPrimaryAction('Ok');
        this.data.secondaryAction = undefined;
        this.data.title = title;
        this.data.body = message;
        this.modal.open();
    };
    /**
     * Apresenta a mensagem. Utilizado para mensagens de alerta.
     * @param message mensagem
     * @param title título da mensagem, padrão 'Atenção'
     */
    HubMessageComponent.prototype.openMessageWarning = function (message, title) {
        if (title === void 0) { title = 'Atenção'; }
        this.data.typeMessage = 'warning';
        this.data.primaryAction = this.getPrimaryAction('Ok');
        this.data.secondaryAction = undefined;
        this.data.title = title;
        this.data.body = message;
        this.modal.open();
    };
    /**
     * Realiza uma pergunta ao usuário.
     * @param message mensagem
     * @param title título da mensagem
     * @returns EventEmitter<boolean>
     * @example
     * .openMessageQuestion(message, title).subscribe((confirm) => {console.log(confirm)})
     */
    HubMessageComponent.prototype.openMessageQuestion = function (message, title, options) {
        if (options === void 0) { options = []; }
        this.data.typeMessage = 'question';
        this.data.primaryAction = this.getPrimaryAction('Sim');
        this.data.secondaryAction = this.getSecondaryAction('Não');
        this.data.title = title;
        this.data.body = message;
        this.inputCheck = options;
        for (var _i = 0, options_1 = options; _i < options_1.length; _i++) {
            var opt = options_1[_i];
            this.checkOpts.push({ label: opt.label, value: opt.label });
            if (opt.checked) {
                this.checkValues.push(opt.label);
            }
        }
        this.modal.open();
        this.modalEvent = new core_1.EventEmitter();
        return this.modalEvent;
    };
    /**
    * Função disparada quando a ação principal for clicada pelo usuário
    */
    HubMessageComponent.prototype.emitPrimary = function () {
        var _this = this;
        this.inputCheck.forEach(function (opt) {
            if (_this.checkValues.findIndex(function (value) { return opt.label === value; }) > -1) {
                opt.checked = true;
            }
            else {
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
    };
    /**
    * Função disparada quando a ação secundária for clicada pelo usuário
    */
    HubMessageComponent.prototype.emitSecondary = function () {
        var _this = this;
        this.inputCheck.forEach(function (opt) {
            if (_this.checkValues.findIndex(function (value) { return opt.label === value; }) > -1) {
                opt.checked = true;
            }
            else {
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
    };
    __decorate([
        core_1.ViewChild(thf_modal_component_1.ThfModalComponent),
        __metadata("design:type", thf_modal_component_1.ThfModalComponent)
    ], HubMessageComponent.prototype, "modal", void 0);
    HubMessageComponent = __decorate([
        core_1.Component({
            selector: 'app-hub-message',
            templateUrl: './hub-message.component.html',
            styleUrls: ['./hub-message.component.css']
        })
        /**
         * @description Componente de mensagens.
         * Pode ser utilizado como instância única, desde que declarado no app.component.
         */
        ,
        __metadata("design:paramtypes", [])
    ], HubMessageComponent);
    return HubMessageComponent;
}());
exports.HubMessageComponent = HubMessageComponent;
//# sourceMappingURL=hub-message.component.js.map