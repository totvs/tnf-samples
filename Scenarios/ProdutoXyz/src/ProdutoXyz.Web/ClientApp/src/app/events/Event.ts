export interface IEvent<TArgs> {
    subscribe(fn: (args: TArgs) => void): void;

    unsubscribe(fn: (args: TArgs) => void): void;
}

export class EventDispatcher<TArgs> implements IEvent<TArgs> {
    private _subscriptions: Array<(args: TArgs) => void> = new Array<(args: TArgs) => void>();
    private dispatched: boolean = false;
    private dispatchArgs: TArgs = undefined;

    subscribe(fn: (args: TArgs) => void): void {
        if (fn) {
            if (this.dispatched)
                fn(this.dispatchArgs);
            else
                this._subscriptions.push(fn);
        }
    }

    unsubscribe(fn: (args: TArgs) => void): void {
        let i = this._subscriptions.indexOf(fn);
        if (i > -1) {
            this._subscriptions.splice(i, 1);
        }
    }

    dispatch(args: TArgs): void {
        if (!this.dispatched) {
            for (let handler of this._subscriptions) {
                handler(args);
            }
            this.dispatched = true;
            this.dispatchArgs = args;
        }
    }
}