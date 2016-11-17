export class ElectronAppFake {
    eventCallbacks: {};
    hasQuit: boolean;

    constructor() {
        this.eventCallbacks = {};
        this.hasQuit = false;
    }

    on(event: string, callback: () => void) {
        this.eventCallbacks[event] = callback;
    }

    quit() {
        this.hasQuit = true;
    }
}