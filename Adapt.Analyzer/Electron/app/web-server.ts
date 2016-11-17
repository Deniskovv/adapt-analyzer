import { ChildProcess } from 'child_process';

export class WebServer {
    private process: ChildProcess;

    constructor(private spawn) {

    }

    start(port: number) {
        this.process = this.spawn('Adapt.Analyzer.Api.Host.exe', [`${port}`]);
        this.process.stdout.on('data', data => console.log(`INFO: ${data}`));
        this.process.stderr.on('data', data => console.error(`ERROR: ${data}`));
    }   

    stop() {
        this.process.kill();
    }
}