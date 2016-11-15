var path = require('path');
var gulp = require('gulp');
var rimraf = require('rimraf');
var spawn = require('child_process').spawn;

var paths = {
    nuget: path.join('C:', 'ProgramData', 'chocolatey', 'bin', 'nuget.exe'),
    msbuild: path.join('C:', 'Program Files (x86)', 'MSBuild', '14.0', 'Bin', 'MsBuild.exe'),
    solution: path.join('..', '..', 'Adapt.Analyzer.sln'),
    webdir: path.join('..', 'Web'),
    releasedir: path.join('..', 'bin', 'Release'),
    webdist: path.join('..', 'Web', 'dist'),
    electronmain: path.join(__dirname, 'main.js'),
    destination: path.join(__dirname, 'dist')
};

function startProcess(options) {
    var cwd = options.cwd ? options.cwd : '.';
    var process = spawn(options.cmd, options.args, { shell: true, cwd: cwd });
    process.on('close', () => {
        options.callback();
    });
}

gulp.task('clean', function(cb) {
    rimraf(paths.destination, cb);
})

gulp.task('nuget-restore', function (cb) {
    var options = {
        cmd: `"${paths.nuget}"`,
        args: [
            'restore',
            paths.solution
        ],
        callback: cb
    };
    startProcess(options);
});

gulp.task('build-api', ['clean', 'nuget-restore'], function (cb) {
    var options = {
        cmd: `"${paths.msbuild}"`,
        callback: cb,
        args: [
            paths.solution,
            '/t:rebuild',
            '/p:Configuration=Release'
        ]
    };
    startProcess(options);
});

gulp.task('build-web', ['clean'], function (cb) {
    var options = {
        cmd: 'npm',
        args: ['run', 'build'],
        callback: cb,
        cwd: paths.webdir
    }
    startProcess(options);
});

gulp.task('copy-api', ['build-api'], function () {
    return gulp.src(paths.releasedir + '/**/*')
        .pipe(gulp.dest(paths.destination));
})

gulp.task('copy-web', ['build-web'], function () {
    return gulp.src(paths.webdist + '/**/*')
        .pipe(gulp.dest(paths.destination));
});

gulp.task('copy-electron', ['copy-api', 'copy-web'], function () {
    return gulp.src(paths.electronmain)
        .pipe(gulp.dest(paths.destination));
})

gulp.task('default', ['clean', 'nuget-restore', 'build-api', 'build-web', 'copy-api', 'copy-web', 'copy-electron']);