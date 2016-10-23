module.exports = function(config) {
  config.set({
    basePath: '',
    frameworks: ['jasmine'],
    files: [
      'src/spec-bundle.js'
    ],
    exclude: [
    ],
    preprocessors: {
      'src/spec-bundle.js': ['webpack', 'sourcemap']
    },
    reporters: ['spec'],
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    autoWatch: true,
    browsers: ['PhantomJS'],
    singleRun: false,
    concurrency: Infinity,
    webpack: require('./webpack.test')
  })
}
