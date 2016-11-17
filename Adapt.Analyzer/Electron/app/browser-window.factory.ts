import { BrowserWindow } from 'electron';

export class BrowserWindowFactory {
    create(url: string) {
        let browserWindow = new BrowserWindow();
        browserWindow.loadURL(url);
        return browserWindow;
    }
}