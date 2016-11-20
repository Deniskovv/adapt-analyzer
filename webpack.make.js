var path = require('path');
var webpack = require('webpack');

module.exports = function make(env) {
    return {
        devtool: getDevtool(env),
        entry: getEntry(env),
        output: {
            path: path.join(__dirname, 'dist'),
            filename: 'js/[name].js',
            sourceMapFilename: '[file].map'
        },
        resolve: {
            extensions: ['', '.ts', '.js', '.scss', '.css', '.html', '.json']
        },
        plugins: getPlugins(env),
        module: {
            loaders: [
                { test: /\.ts$/, loader: 'ts', exclude: /node_modules/ },
                { test: /\.(scss|css)$/, loader: 'style!css!sass' },
                { test: /\.html$/, loader: 'html' }
            ]
        }
    }
}

function getEntry(env) {
    return {
        app: './app/main.ts',
        vendor: './app/vendor.ts'
    }
}

function getDevtool(env) {
    if (isProd(env))
        return 'cheap-module-source-map';

    return 'source-map';
}

function getPlugins(env) {
    var plugins = [
        new webpack.optimize.CommonsChunkPlugin({
            name: 'vendor',
            minChunks: Infinity
        })
    ];

    if (isProd(env)) {
        plugins.push(new webpack.optimize.DedupePlugin());
        plugins.push(new webpack.optimize.OccurenceOrderPlugin());
        plugins.push(new webpack.optimize.UglifyJsPlugin())
    }
    return plugins;
}

function isProd(env) {
    return env === 'prod';
}