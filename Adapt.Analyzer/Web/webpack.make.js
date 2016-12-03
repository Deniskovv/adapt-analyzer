var path = require('path');
var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var CopyWebpackPlugin = require('copy-webpack-plugin');

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
            extensions: ['', '.ts', '.js', '.scss', '.css', '.html', '.json'],
            alias: getAlias(env)
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
    if (isTest(env))
        return undefined;

    return {
        app: './src/main.ts',
        vendor: './src/vendor.ts'
    }
}

function getDevtool(env) {
    if (isTest(env))
        return undefined;

    if (isProd(env))
        return 'cheap-module-source-map';

    return 'source-map';
}

function getAlias(env) {
    if (!isTest(env))
        return undefined;
    
    return {
        'google': path.resolve(__dirname, './src/fakes/google-maps.fake.ts')
    };
}

function getPlugins(env) {
    var plugins = [
        new HtmlWebpackPlugin({
            filename: 'index.html',
            template: 'src/index.html',
            inject: 'body'
        })
    ];

    if (isTest(env)) {
        plugins.push(new webpack.SourceMapDevToolPlugin({
            filename: null,
            test: /\.(ts|js)($|\?)/i
        }))

        plugins.push(new webpack.ProvidePlugin({
            google: 'google'
        }))
        return plugins;
    }

    plugins.push(new webpack.optimize.CommonsChunkPlugin({
        name: 'vendor',
        minChunks: Infinity
    }));
    plugins.push(createCopyPlugin(env));

    if (isProd(env)) {
        plugins.push(new webpack.optimize.DedupePlugin());
        plugins.push(new webpack.optimize.OccurenceOrderPlugin());
        plugins.push(new webpack.optimize.UglifyJsPlugin())
    }
    return plugins;
}

function createCopyPlugin(env) {
    var copy_from = isProd(env) ? 'config.prod.json' : 'config.local.json';
    return new CopyWebpackPlugin([
        { from: copy_from, to: 'config.json' }
    ]);
}

function isProd(env) {
    return env === 'prod';
}

function isTest(env) {
    return env === 'test';
}