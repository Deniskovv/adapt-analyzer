export class BrowserWindowFake {
    eventCallbacks: { };
    loadedURLs: string[];

    constructor() {
        this.eventCallbacks = {};
        this.loadedURLs = [];
    }

    loadURL(url: string) {
        this.loadedURLs.push(url);
    }

    on(event: string, callback: () => void) {
        this.eventCallbacks[event] = callback;
    }
}