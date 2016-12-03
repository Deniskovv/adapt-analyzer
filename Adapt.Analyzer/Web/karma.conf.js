module.exports = function (config) {
  config.set({
    basePath: '',
    frameworks: ['jasmine'],
    files: [
      'https://maps.googleapis.com/maps/api/js',
      './src/spec-bundle.ts'
    ],
    exclude: [
    ],
    preprocessors: {
      './src/spec-bundle.ts': ['webpack']
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
