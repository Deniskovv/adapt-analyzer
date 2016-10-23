var SpecReporter = require('jasmine-spec-reporter');

exports.config = {
  allScriptsTimeout: 11000,
  specs: [
    './scenarios/**/*.spec.ts'
  ],
  capabilities: {
    'browserName': 'chrome'
  },
  directConnect: true,
  baseUrl: 'http://localhost:4200/',
  framework: 'jasmine',
  jasmineNodeOpts: {
    showColors: true,
    defaultTimeoutInterval: 30000,
    print: function() {}
  },
  useAllAngular2AppRoots: true,
  beforeLaunch: function() {
    require('ts-node').register();

    var Server = require('./helpers').Server;
    this.server = new Server();
    this.server.start();
  },
  
  onCleanUp: function() {
    this.server.stop();
  },
  onPrepare: function() {
    jasmine.getEnv().addReporter(new SpecReporter());
  }
};