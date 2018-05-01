"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var ngx_treeview_1 = require("ngx-treeview");
var RoleTreeviewI18n = /** @class */ (function (_super) {
    __extends(RoleTreeviewI18n, _super);
    function RoleTreeviewI18n() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    RoleTreeviewI18n.prototype.getText = function (selection) {
        if (selection.uncheckedItems.length === 0)
            return this.getAllCheckboxText();
        switch (selection.checkedItems.length) {
            case 0:
                return 'Selecione as opções';
            case 1:
                return selection.checkedItems[0].text;
            default:
                return selection.checkedItems.length + " op\u00E7\u00F5es selecionadas";
        }
    };
    RoleTreeviewI18n.prototype.getAllCheckboxText = function () {
        return "Todas as permissões";
    };
    RoleTreeviewI18n.prototype.getFilterPlaceholder = function () {
        return "Filtrar permissões";
    };
    RoleTreeviewI18n.prototype.getFilterNoItemsFoundText = function () {
        return "Nenhuma permissão encontrada";
    };
    RoleTreeviewI18n.prototype.getTooltipCollapseExpandText = function (isCollapse) {
        return isCollapse ? "Expandir" : "Contrair";
    };
    RoleTreeviewI18n = __decorate([
        core_1.Injectable()
    ], RoleTreeviewI18n);
    return RoleTreeviewI18n;
}(ngx_treeview_1.TreeviewI18n));
exports.RoleTreeviewI18n = RoleTreeviewI18n;
//# sourceMappingURL=role-treeview-i18n.js.map