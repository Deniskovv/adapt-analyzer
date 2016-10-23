import { spawn, ChildProcess } from 'child_process';
import { join } from 'path';
let deasync = require('deasync');

export class Server {
    static root_dir = join(__dirname, '..', '..');
    static analyzer_dir = join(Server.root_dir, 'Adapt.Analyzer');
    static api_dir = join(Server.analyzer_dir, 'Api');
    static release_dir = join(Server.analyzer_dir, 'bin', 'Release');
    static web_dir = join(Server.analyzer_dir, 'Web');
    static api_exe = join(Server.analyzer_dir, 'bin', 'Release', 'Adapt.Analyzer.Api.Host.exe');
    static ms_build_path = `"${join('C:', 'Program Files (x86)', 'msbuild', '14.0', 'bin', 'msbuild.exe')}"`
    api_process: ChildProcess

    start() {
        console.log('Starting Server...')
        this.build();
        this.start_api();
        console.log('Server Started');
    }

    stop() {
        this.api_process.kill();
    }

    private build() {
        let web_built = false;
        this.run_command('npm', ['run build'], Server.web_dir).on('close', () => web_built = true);
        
        let api_built = false;
        this.run_command(Server.ms_build_path, [
            join(Server.analyzer_dir, 'Adapt.Analyzer.sln'),
            '/t:rebuild',
            '/p:Configuration=Release'
        ]).on('close', () => api_built = true);
        deasync.loopWhile(() => !api_built || !web_built);
    }

    private start_api() {
        let api_running = false;
        this.api_process = this.run_command(Server.api_exe, ['4200'], Server.release_dir);
        this.api_process.stdout.on('data', (data: string) => {
            console.log(`API: ${data}`)
            if (data.indexOf('Now Listening') > -1)
                api_running = true;
        });
        
        this.api_process.stderr.on('data', data => {
            console.error(`API ERROR: ${data}`);
        });
        deasync.loopWhile(() => !api_running);
    }

    private run_command(cmd, args = [], working_dir = '') {
        console.log(`Starting ${cmd} with args: ${args.join(' ')}...`)
        let child_process = spawn(cmd, args, {
            cwd: working_dir,
            shell: true
        });
        return child_process;
    }
}