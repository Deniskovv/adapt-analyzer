import { ChildProcess, SpawnOptions } from 'child_process';
import { join } from 'path';

export class WebServer {
    private process: ChildProcess;

    constructor(private spawn) {

    }

    start(port: number) {
        let options: SpawnOptions = {
            cwd: __dirname
        }
        this.process = this.spawn(join(__dirname, 'Adapt.Analyzer.Api.Host.exe'), [`${port}`], options);
        this.process.stdout.on('data', data => console.log(`INFO: ${data}`));
        this.process.stderr.on('data', data => console.error(`ERROR: ${data}`));
    }   

    stop() {
        this.process.kill();
    }
}