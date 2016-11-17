import { 
    ElectronAppFake, 
    BrowserWindowFake 
} from '../fakes';

import { WebServer } from './web-server';
import { BrowserWindowFactory } from './browser-window.factory'; 
import { App } from './app';

describe('App', () => {
    let app: App;
    let electronApp: ElectronAppFake;
    let browserWindow: BrowserWindowFake;
    let webServer: WebServer;
    let browserWindowFactory: BrowserWindowFactory;

    beforeEach(() => {
        browserWindow = new BrowserWindowFake();
        browserWindowFactory = new BrowserWindowFactory();
        spyOn(browserWindowFactory, 'create').and.returnValue(browserWindow);

        webServer = new WebServer(null);
        spyOn(webServer, 'start').and.callFake(() => {});
        spyOn(webServer, 'stop').and.callFake(() => {});

        electronApp = new ElectronAppFake();
        app = new App(electronApp, browserWindowFactory, webServer);;
    });

    it('should create browser window', () => {
        app.start();
        electronApp.eventCallbacks['ready']();
        expect(browserWindowFactory.create).toHaveBeenCalledWith('http://localhost:5555/')
    });

    it('should get rid of window when closed', () => {
        app.start();
        electronApp.eventCallbacks['ready']();
        browserWindow.eventCallbacks['closed']();
        expect(app.browserWindow).toBeUndefined();
        expect(webServer.stop).toHaveBeenCalled();
    });

    it('should quit app on all windows closed', () => {
        app.start();
        electronApp.eventCallbacks['window-all-closed']();
        expect(electronApp.hasQuit).toBe(true);
        expect(webServer.stop).toHaveBeenCalled();
    });

    it('should start adapt analyzer web server', () => {
        app.start();
        expect(webServer.start).toHaveBeenCalledWith(5555);
    })
});