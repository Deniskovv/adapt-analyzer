import * as child_process from 'child_process';

import { WebServer } from './web-server';

describe('WebServer', () => {
    let process: { kill(): void, isKilled: boolean };
    let webServer: WebServer;

    beforeEach(() => {
        process = {
            isKilled: false,
            kill() {
                this.isKilled = true;
            }
        }
        spyOn(child_process, 'spawn').and.callFake(() => {
            return process;
        });

        webServer = new WebServer(child_process.spawn);
    });

    it('should spawn adapt analyzer server', () => {
        webServer.start(5555);
        expect(child_process.spawn).toHaveBeenCalledWith('./Adapt.Analyzer.Api.Host.exe', ['5555']);
    });

    it('should kill adapt analyzer server', () => {
        webServer.start(5555);
        webServer.stop();
        expect(process.isKilled).toBe(true);
    })
});