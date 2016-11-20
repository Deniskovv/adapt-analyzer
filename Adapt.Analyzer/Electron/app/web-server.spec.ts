import * as child_process from 'child_process';
import * as path from 'path';

import { WebServer } from './web-server';

describe('WebServer', () => {
    let process: {
        events: {},
        kill(): void,
        isKilled: boolean,
        stdout: {
            events: {},
            on(event: string, cb: () => void): void
        },
        stderr: {
            events: {},
            on(event: string, cb: () => void): void
        }
    };
    let webServer: WebServer;

    beforeEach(() => {
        process = {
            events: {},
            isKilled: false,
            kill() {
                this.isKilled = true;
            },
            stdout: {
                events: {},
                on(event, cb) {
                    this.events[event] = cb
                }
            },
            stderr: {
                events: {},
                on(event, cb) {
                    this.events[event] = cb
                }
            }
        }
        spyOn(child_process, 'spawn').and.callFake(() => {
            return process;
        });

        webServer = new WebServer(child_process.spawn);
    });

    it('should spawn adapt analyzer server', () => {
        webServer.start(5555);
        expect(child_process.spawn).toHaveBeenCalledWith(path.join(__dirname, './Adapt.Analyzer.Api.Host.exe'), 
            ['5555'],
            { cwd: __dirname });
    });

    it('should kill adapt analyzer server', () => {
        webServer.start(5555);
        webServer.stop();
        expect(process.isKilled).toBe(true);
    })
});