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
var forms_1 = require("@angular/forms");
var app_component_1 = require("../app.component");
/**
 * @description HubCrudComponentList
 * Classe abstrata para o componente de listagem de dados
 * @extends HubEntity
 */
var HubCrudComponentList = /** @class */ (function () {
    /**
     * Método construtor que inicializa o objeto
     * @param router
     * @param dataService
     * @param Entity
     */
    function HubCrudComponentList(router, dataService, Entity) {
        var _this = this;
        this.router = router;
        this.dataService = dataService;
        this.Entity = Entity;
        /**
         * thfListActions: atributo com as ações dos registros lista de dados
         */
        this.thfListActions = [
            { action: 'update', label: 'Editar' },
            { action: 'delete', label: 'Excluir' }
        ];
        /** thfFilter: atributo que define as ações de filtro */
        this.thfFilter = {
            placeholder: 'Busca',
            action: this.filterAction,
            ngModel: 'quickSearch',
        };
        this.thfAdvancedSearchName = "Filtrar Busca";
        /**
         * Ação primária do modal de busca avançada
         */
        this.advancedSearchPrimaryAction = {
            label: 'Confirmar',
            action: function () {
                _this.confirmAdvancedSearch();
            }
        };
        /**
         * Ação secundária do modal de busca avançada
         */
        this.advancedSearchSecondaryAction = {
            label: 'Cancelar',
            action: function () {
                _this.cancelAdvancedSearch();
            }
        };
    }
    /**
     * Método padrão do Angular executado ao inicializar o componente
     */
    HubCrudComponentList.prototype.ngOnInit = function () {
        if (this.filterInit) {
            this.filterAction(true);
        }
        else {
            this.dataService.page = 1;
            this.dataService.getItems();
        }
    };
    /**
     * Método que possui as ações de filtro de dados
     * @param isAdvancedSearch
     */
    HubCrudComponentList.prototype.filterAction = function (isAdvancedSearch) {
        if (isAdvancedSearch === void 0) { isAdvancedSearch = false; }
        this.dataService.page = 1;
        this.setFilters(isAdvancedSearch);
        this.dataService.getItems();
    };
    /**
     * método que abre o modal de pesquisa avançada
     */
    HubCrudComponentList.prototype.openAdvancedSearch = function () {
        this.thfAdvancedSearch.open();
    };
    /**
     * método que cancela e fecha o modal de pesquisa avançada
     */
    HubCrudComponentList.prototype.cancelAdvancedSearch = function () {
        this.thfAdvancedSearch.close();
    };
    /** método que confirma e fecha o modal de pesquisa avançada */
    HubCrudComponentList.prototype.confirmAdvancedSearch = function () {
        if (this.isFormValid()) {
            this.thfAdvancedSearch.close();
            this.filterAction(true);
        }
    };
    /**
     * método que trata o registro que será enviado para edição
     * @param item item/registro da lista
     */
    HubCrudComponentList.prototype.update = function (item) {
        if (item) {
            item = new this.Entity(item);
            this.updateRecord(item.id);
        }
    };
    /**
     * método que trata o registro que será enviado para exclusão
     * @param item item/registro da lista
     */
    HubCrudComponentList.prototype.delete = function (item) {
        if (item) {
            item = new this.Entity(item);
            this.deleteRecord(item.id);
        }
    };
    /**
     * método que valida o registro passado por parâmetro e faz a abertura da página de edição
     * @param id parâmetro que recebe o id do registro que será editado
     */
    HubCrudComponentList.prototype.updateRecord = function (id) {
        var rota = [this.dataService.serviceName + "/edit"].concat(id.toString());
        this.router.navigate(rota);
    };
    /**
     * método que valida o registro passado por parâmetro antes da exclusão
     * @param id parâmetro que recebe o id do registro que será excluído
     * @param confirmation indica se deve ser exibida a mensagem de confirmação
     */
    HubCrudComponentList.prototype.deleteRecord = function (id, confirmation) {
        if (confirmation === void 0) { confirmation = true; }
        var self = this;
        app_component_1.hubMessage.openMessageQuestion('Deseja realmente excluir o registro selecionado?', 'Exclusão')
            .subscribe(function (confirm) {
            if (confirm) {
                self.dataService.delete(id)
                    .subscribe(function (data) {
                    if (data && data.messages !== null) {
                        if (data.messages[0].code >= 400) {
                            app_component_1.hubMessage.openMessageError(data.messages[0].detail);
                            return;
                        }
                        app_component_1.hubMessage.openMessageInformation('Registro excluído com sucesso!');
                    }
                    self.dataService.page = 1;
                    self.dataService.getItems();
                });
            }
        });
    };
    /**
     * método que valida se o formulário está válido
     */
    HubCrudComponentList.prototype.isFormValid = function () {
        var control;
        var ret = true;
        // tslint:disable-next-line:forin
        for (var x in this.form.form.controls) {
            control = this.form.form.controls[x];
            if (control.errors) {
                control.markAsDirty({ onlySelf: true });
                ret = false;
            }
        }
        return ret;
    };
    __decorate([
        core_1.ViewChild('form'),
        __metadata("design:type", forms_1.NgForm)
    ], HubCrudComponentList.prototype, "form", void 0);
    return HubCrudComponentList;
}());
exports.HubCrudComponentList = HubCrudComponentList;
//# sourceMappingURL=hub-crud-component-list.js.map