var path = require('path');
var gulp = require('gulp');
var zip = require('gulp-zip');
var jasmine = require('gulp-jasmine');
var rimraf = require('rimraf');
var spawn = require('child_process').spawn;
var ts = require('gulp-typescript');
var package_json = require('./package.json');

var paths = {
    nuget: path.join('C:', 'ProgramData', 'chocolatey', 'bin', 'nuget.exe'),
    msbuild: path.join('C:', 'Program Files (x86)', 'MSBuild', '14.0', 'Bin', 'MsBuild.exe'),
    solution: path.join('..', '..', 'Adapt.Analyzer.sln'),
    webdir: path.join('..', 'Web'),
    releasedir: path.join('..', 'bin', 'Release'),
    webdist: path.join('..', 'Web', 'dist'),
    packagedir: path.join(__dirname, 'adapt-analyzer-win32-x64'),
    package_json: path.join(__dirname, 'package.json'),
    electronapp: path.join(__dirname, 'app', '**', '*.ts'),
    destination: path.join(__dirname, 'dist')
};

function startProcess(options) {
    var cwd = options.cwd ? options.cwd : '.';
    var process = spawn(options.cmd, options.args, { shell: true, cwd: cwd });
    process.on('close', () => {
        options.callback();
    });
}

gulp.task('clean-dist', function (cb) {
    rimraf(paths.destination, cb);
});

gulp.task('clean-package', function (cb) {
    rimraf(paths.packagedir, cb);
});

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

gulp.task('build-electron', ['clean'], function(cb) {
    var project = ts.createProject('./tsconfig.json');
    var result = gulp.src(paths.electronapp)
        .pipe(project());

    return result.js.pipe(gulp.dest(paths.destination));
});

gulp.task('copy-api', ['build'], function () {
    return gulp.src(paths.releasedir + '/**/*')
        .pipe(gulp.dest(paths.destination));
});

gulp.task('copy-web', ['build'], function () {
    return gulp.src(paths.webdist + '/**/*')
        .pipe(gulp.dest(paths.destination));
});

gulp.task('copy-electron', ['build'], function () {
    return gulp.src(paths.package_json)
        .pipe(gulp.dest(paths.destination));
});

gulp.task('package-app', ['clean', 'build', 'copy'], function(cb) {
    var options = {
        cmd: '"./node_modules/.bin/electron-packager" ./dist adapt-analyzer --platform=win32',
        callback: cb,
        args: []
    };
    startProcess(options);
});

gulp.task('test-electron', function () {
    return gulp.src('./run_tests.js')
        .pipe(jasmine());
});

gulp.task('zip-package', ['package-app'], function() {
    return gulp.src(paths.packagedir)
        .pipe(zip(`adapt-analyzer-win32-x64-${package_json.version}.zip`))
        .pipe(gulp.dest('dist'));
});


gulp.task('clean', ['clean-dist', 'clean-package']);
gulp.task('build', ['clean', 'build-api', 'build-web', 'build-electron']);
gulp.task('copy', ['build', 'copy-api', 'copy-web', 'copy-electron']);
gulp.task('default', ['clean', 'build', 'copy']);
gulp.task('package', ['default', 'package-app', 'zip-package'])