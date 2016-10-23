import { spawn, ChildProcess } from 'child_process';
import { join } from 'path';
var deasync = require('deasync');

export class Server {
    static root_dir = join(__dirname, '..', '..');
    static analyzer_dir = join(Server.root_dir, 'Adapt.Analyzer');
    static api_dir = join(Server.analyzer_dir, 'Api');
    static web_dir = join(Server.analyzer_dir, 'Web');
    static api_exe = join(Server.analyzer_dir, 'bin', 'Release', 'Adapt.Analyzer.Api.Host.exe');
    static ms_build_path = `"${join('C:', 'Program Files (x86)', 'msbuild', '14.0', 'bin', 'msbuild.exe')}"`
    api_process: ChildProcess

    start() {
        this.build();
        this.start_api();
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
        deasync.loopWhile(() => !api_built && !web_built);
    }

    private start_api() {
        let api_running = false;
        this.api_process = this.run_command(Server.api_exe, ['4200']);
        this.api_process.stdout.on('data', (data: string) => {
            console.log(`API: ${data}`)
            if (data.indexOf('Now Listening') > -1)
                api_running = true;
        });
        deasync.loopWhile(() => !api_running);
    }

    private run_command(cmd, args = [], working_dir = '') {
        let child_proc = spawn(cmd, args, {
            cwd: working_dir,
            shell: true
        });
        child_proc.on('data', data => console.log(`OUT ${cmd}: ${data}\n`));
        child_proc.stdout.on('data', data => console.log(`INFO ${cmd}: ${data}\n`));
        child_proc.stderr.on('data', data => console.error(`ERROR ${cmd}: ${data}\n`)); 
        return child_proc;
    }
}