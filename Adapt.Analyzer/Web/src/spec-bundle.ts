require('./vendor');

require('zone.js/dist/proxy');
require('zone.js/dist/sync-test');
require('zone.js/dist/jasmine-patch');
require('zone.js/dist/async-test');
require('zone.js/dist/fake-async-test');

import { NgModule } from '@angular/core';
import { XHRBackend } from '@angular/http'

import { TestBed } from '@angular/core/testing';
import { MockBackend, MockConnection } from '@angular/http/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from '@angular/platform-browser-dynamic/testing';

import { AppModule } from './app/app.module';

@NgModule({
    imports: [
        BrowserDynamicTestingModule,
        AppModule
    ],
    providers:[
        { provide: XHRBackend, useClass: MockBackend}
    ]
})
class TestingModule {}

describe('Adapt Analyzer', () => {
    beforeAll(() => {
        TestBed.initTestEnvironment(TestingModule, platformBrowserDynamicTesting())
    })

    var context = (<any>require).context('.', true, /\.spec\.ts/);
    context.keys().forEach(context);
})

