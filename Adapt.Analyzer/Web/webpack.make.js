var path = require('path');
var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = function make(env) {
    return {
        devtool: get_dev_tool(env),
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
                { test: /\.(sass|css)$/, loader: 'raw!css!sass' },
                { test: /\.html$/, loader: 'html' },
                { test: /\.json$/, loader: 'json' }
            ]
        }
    };
}

function get_dev_tool(env) {
    if (is_test(env))
        return 'inline-source-map';

    if (is_prod(env))
        return 'cheap-module-source-map';
    
    return 'source-map';
}

function get_plugins(env) {
    let plugins = [
        new webpack.DefinePlugin({
            'process.env': {
                'ENV': JSON.stringify(env)
            }
        }),
        new HtmlWebpackPlugin({
            filename: 'index.html',
            template: 'src/index.html',
            inject: 'body'
        })
    ];

    if (is_test(env))
        return plugins;


    plugins.push(new webpack.optimize.CommonsChunkPlugin({
        name: 'vendor',
        minChunks: Infinity
    }))
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
        vendor: './src/vendor.ts',
        app: './src/main.ts'
    };
}

function is_test(env) {
    return env === 'test';
}

function is_prod(env) {
    return env === 'prod';
}