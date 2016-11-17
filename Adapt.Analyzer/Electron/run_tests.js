require('ts-node/register');

var SpecReporter = require('jasmine-spec-reporter');
var Jasmine = require('jasmine');
var jasmine = new Jasmine();

jasmine.loadConfig({
    spec_dir: '.',
    spec_files: [
        'app/**/*.spec.ts'
    ]
});

jasmine.configureDefaultReporter({print: null});
jasmine.env.clearReporters();
jasmine.addReporter(new SpecReporter());
jasmine.execute();