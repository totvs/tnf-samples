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
var forms_1 = require("@angular/forms");
var core_1 = require("@angular/core");
var app_component_1 = require("../app.component");
/**
 * @description
 * Classe abstrata que implementa a interface IHubCrudComponentForm, implementando os métodos padrão.
 */
var HubCrudComponentForm = /** @class */ (function () {
    /**
     * Construtor para inicialização da classe.
     * @param activatedRoute Objeto para obter os parâmetros da URL.
     * @param router Parâmetro para navegação entre as rotas.
     * @param dataService Serviço de dados da entidade a ser manipulada.
     * @param Entity Entidade a ser manipulada.
     */
    function HubCrudComponentForm(activatedRoute, router, dataService, Entity) {
        this.activatedRoute = activatedRoute;
        this.router = router;
        this.dataService = dataService;
        this.Entity = Entity;
        /**
         * Atributo indica se o registro filho foi modificado.
         */
        this.childModified = false;
        /**
         * Atributo indica se irá fechar a tela ao salvar.
         */
        this.closeOnSave = true;
        this.isUpdate = false;
        /**
         * Evento emitido antes de editar;
         */
        this.beforeEditing = new core_1.EventEmitter();
        /**
         * Evento emitido antes de editar;
         */
        this.beforePersisting = new core_1.EventEmitter();
        /**
         * Comportamentos para formulário filho de outro
         */
        this.asGrandChild = false;
        this.data = new Entity(undefined);
    }
    /**
     * Método executado ao iniciar o componente, realizando os tratamentos iniciais, como leitura de parâmetros
     * da URL.
     */
    HubCrudComponentForm.prototype.ngOnInit = function () {
        var _this = this;
        this.activatedRoute.queryParams.subscribe(function (params) {
            if (params['_back']) {
                _this.lastUrl = params['_back'];
                _this.asGrandChild = true;
            }
        });
        this.activatedRoute.params.subscribe(function (params) {
            _this.paramId = undefined;
            if (params['id'] !== undefined) {
                _this.paramId = params['id'];
                _this.isUpdate = true;
            }
            _this.params = params;
            if (_this.paramId !== undefined) {
                _this.saveNew = undefined;
                _this.dataService.get(_this.paramId).subscribe(function (res) {
                    _this.data = new _this.Entity(res);
                    _this.beforeEditing.emit(_this.data);
                });
            }
            else
                _this.beforeEditing.emit(_this.data);
        });
    };
    /**
     * Método executado ao voltar.
     * @param saveNew Indica que a ação está em sendo executada ao utilizar o botão "Salvar e Novo".
     * @param cancel Indica que está sendo executado a partir do botão "Cancelar".
     */
    HubCrudComponentForm.prototype.back = function (saveNew, cancel) {
        if (saveNew === void 0) { saveNew = false; }
        if (cancel === void 0) { cancel = false; }
        this.childModified = false;
        if (saveNew === true) {
            this.data = new this.Entity(undefined);
            if (this.form) {
                this.form.resetForm(this.data);
            }
        }
        else {
            if (cancel || this.closeOnSave) {
                if (this.asGrandChild) {
                    this.router.navigate([this.lastUrl]);
                }
                else {
                    this.router.navigate([this.dataService.serviceName]);
                }
            }
            else {
                if (!this.asGrandChild) {
                    var rota = [this.dataService.serviceName + "/edit"].concat(this.paramId.toString());
                    this.router.navigate(rota);
                }
            }
        }
    };
    /**
     * Método retorna se o formulário teve algum dado alterado.
     */
    HubCrudComponentForm.prototype.isFormChanged = function () {
        var control;
        if (this.childModified) {
            return true;
        }
        if (this.form) {
            // tslint:disable-next-line:forin
            for (var x in this.form.form.controls) {
                control = this.form.form.controls[x];
                if (control.dirty) {
                    return true;
                }
            }
        }
        return false;
    };
    /**
     * Marca os campos como Pristine
     */
    HubCrudComponentForm.prototype.markFormPristine = function () {
        for (var x in this.form.form.controls) {
            if (this.form.form.controls) {
                this.form.form.controls[x].markAsPristine();
            }
        }
    };
    /**
     * Método retorna se o formulário é válido.
     */
    HubCrudComponentForm.prototype.isFormValid = function () {
        var control;
        var ret = true;
        if (this.form) {
            // tslint:disable-next-line:forin
            for (var x in this.form.form.controls) {
                control = this.form.form.controls[x];
                if (control.errors) {
                    control.markAsDirty({ onlySelf: true });
                    ret = false;
                }
            }
        }
        return ret;
    };
    /**
     * Método executado ao pressionar o botão "Cancelar".
     */
    HubCrudComponentForm.prototype.cancel = function () {
        var _this = this;
        if (this.isFormChanged() === false) {
            this.back(false, true);
        }
        else {
            app_component_1.hubMessage.openMessageQuestion('Deseja cancelar a edição do registro?', 'Edição').subscribe(function (confirm) {
                if (confirm) {
                    _this.back(false, true);
                }
            });
        }
    };
    /**
     * Método que retorna um post ou put de acordo com o método necessário para a ação atual do
     * formulário (inclusão ou edição).
     */
    HubCrudComponentForm.prototype.getMethod = function () {
        if (this.saveNew === undefined && this.data.id !== undefined) {
            return this.dataService.put(this.data, this.data.id);
        }
        else {
            return this.dataService.post(this.data);
        }
    };
    /**
     * Método que execução inclusão/edição do registro.
     * @param saveNew Parâmetro indica se a opção utilizada foi "Salvar" ou "Salvar e Novo".
     */
    HubCrudComponentForm.prototype.save = function (saveNew) {
        var _this = this;
        if (saveNew === void 0) { saveNew = false; }
        var self = this;
        if (this.isFormChanged() === false
            && this.saveNew === undefined
            && this.cancel !== undefined) {
            app_component_1.hubMessage.openMessageQuestion('Os dados não foram alterados. Continuar editando?', 'Edição').subscribe(function (confirm) {
                if (confirm === false) {
                    self.back(false, true);
                }
            });
        }
        else {
            if (this.isFormValid()) {
                this.beforePersisting.emit(this.data);
                this.getMethod()
                    .subscribe(function (success) {
                    if (success) {
                        if (!!success.messages && success.messages)
                            return;
                        app_component_1.hubMessage.openMessageInformation("Registro " + (_this.saveNew === undefined ? 'alterado' : 'incluído') + " com sucesso!");
                        if (_this.saveNew && success && success.id) {
                            _this.paramId = success.id;
                        }
                        _this.markFormPristine();
                        _this.back(saveNew);
                    }
                });
            }
        }
    };
    /**
     * Método executado ao pressionar o botão "Salvar e Novo".
     */
    HubCrudComponentForm.prototype.saveNew = function () {
        this.save(true);
    };
    /**
     * Método para verificar se o registro filho pode ser editado ou não.
     * Em caso positivo realiza a navegação para a rota apropriada para tal.
     * @param child Serviço de dados do filho.
     * @param childIdValue Array contendo a chave primária do registro filho.
     */
    HubCrudComponentForm.prototype.defaultGrandChildEditHandle = function (child) {
        var childIdValue = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            childIdValue[_i - 1] = arguments[_i];
        }
        if (this.childModified) {
            return app_component_1.hubMessage.openMessageInformation('Salve o registro antes de efetuar essa operação.');
        }
        var rota = [this.dataService.serviceName + "/edit/" + child.serviceName];
        childIdValue.forEach(function (el) {
            rota = rota.concat(el);
        });
        this.router.navigate(rota, { queryParams: { _back: this.router.url } });
    };
    __decorate([
        core_1.ViewChild('form'),
        __metadata("design:type", forms_1.NgForm)
    ], HubCrudComponentForm.prototype, "form", void 0);
    return HubCrudComponentForm;
}());
exports.HubCrudComponentForm = HubCrudComponentForm;
//# sourceMappingURL=hub-crud-component-edit.js.map