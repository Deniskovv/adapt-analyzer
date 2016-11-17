import { format } from 'url';

import { WebServer } from './web-server';
import { BrowserWindowFactory } from './browser-window.factory';

export class App {
    browserWindow;

    constructor(private app, 
    private browserWindowFactory: BrowserWindowFactory,
    private webServer: WebServer) {

    }

    start() {
        this.webServer.start(5555);
        this.app.on('ready', this.createWindow.bind(this));
        this.app.on('window-all-closed', this.onWindowsClosed.bind(this));
    }

    private createWindow() {
        this.browserWindow = this.browserWindowFactory.create('http://localhost:5555/');
        this.browserWindow.on('closed', this.onBrowserClosed.bind(this));
    }

    private onBrowserClosed() {
        this.browserWindow = undefined;
        this.webServer.stop();
    }

    private onWindowsClosed() {
        this.app.quit();
        this.webServer.stop();
    }
}