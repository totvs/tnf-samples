"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var EventDispatcher = /** @class */ (function () {
    function EventDispatcher() {
        this._subscriptions = new Array();
        this.dispatched = false;
        this.dispatchArgs = undefined;
    }
    EventDispatcher.prototype.subscribe = function (fn) {
        if (fn) {
            if (this.dispatched)
                fn(this.dispatchArgs);
            else
                this._subscriptions.push(fn);
        }
    };
    EventDispatcher.prototype.unsubscribe = function (fn) {
        var i = this._subscriptions.indexOf(fn);
        if (i > -1) {
            this._subscriptions.splice(i, 1);
        }
    };
    EventDispatcher.prototype.dispatch = function (args) {
        if (!this.dispatched) {
            for (var _i = 0, _a = this._subscriptions; _i < _a.length; _i++) {
                var handler = _a[_i];
                handler(args);
            }
            this.dispatched = true;
            this.dispatchArgs = args;
        }
    };
    return EventDispatcher;
}());
exports.EventDispatcher = EventDispatcher;
//# sourceMappingURL=event.js.map