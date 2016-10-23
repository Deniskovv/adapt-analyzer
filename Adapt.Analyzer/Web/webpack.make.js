var path = require('path');
var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = function make(env) {
    return {
        devtool: 'source-map',
        entry: get_entry(env),
        plugins: get_plugins(env),
        output: {
            path: get_output_path(env),
            filename: 'js/[name].js',
            sourceMapFilename: '[file].map'
        },
        resolve: {
            extensions: ['', '.ts', '.js', '.sass', '.css', '.html']
        },
        module: {
            loaders: [
                { test: /\.ts$/, loader: 'ts' },
                { test: /\.(sass|css)$/, loader: 'to-string!css!sass' },
                { test: /\.html$/, loader: 'html' },
                { test: /\.json$/, loader: 'json' }
            ]
        }
    };
}

function get_plugins(env) {
    let plugins = [
        new HtmlWebpackPlugin({
            filename: 'index.html',
            template: 'src/index.html',
            inject: 'body'
        })
    ];

    if (is_prod(env)) {
        plugins.push(new webpack.optimize.UglifyJsPlugin());
        plugins.push(new webpack.optimize.DedupePlugin());
        plugins.push(new webpack.optimize.OccurrenceOrderPlugin());
    }
    return plugins;
}

function get_output_path(env) {
    let configuration = is_prod(env) ? 'Release' : 'Debug';
    return path.join('..', 'bin', configuration);
}

function get_entry(env) {
    if (is_test(env))
        return undefined;

    return {
        app: './src/main.ts',
        vendor: './src/vendor.ts'
    };
}

function is_test(env) {
    return env === 'test';
}

function is_prod(env) {
    return env === 'prod';
}