import { spawn } from 'child_process';
import { app, BrowserWindow } from 'electron';
import { App } from './app';
import { WebServer } from './web-server';
import { BrowserWindowFactory } from './browser-window.factory';

let webServer = new WebServer(spawn);
let factory = new BrowserWindowFactory();
let application = new App(app, factory, webServer);
application.start();