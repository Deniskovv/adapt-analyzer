import * as angular from 'angular';

import { datacardState } from '../datacards/datacard.state';
import { UploadComponent } from './upload.component';
import { UploadData } from './models';
import { FileReaderFake } from '../../../fakes/file-reader.fake';

describe('UploadComponent', () => {
    let uploadDirective: angular.IDirective;
    let $httpBackend: angular.IHttpBackendService;
    let controller: UploadComponent;
    let $state: angular.ui.IStateService;
    let fileReaderFake: FileReaderFake;

    beforeEach(angular.mock.inject((_$injector_, _$controller_, _$httpBackend_, _$state_) => {
        uploadDirective = _$injector_.get('uploadDirective')[0];
        $state = _$state_;
        spyOn($state, 'go').and.callFake(() => {});

        fileReaderFake = new FileReaderFake();
        spyOn(window, 'FileReader').and.returnValue(fileReaderFake);

        $httpBackend = _$httpBackend_;
        controller = _$controller_(UploadComponent, {
                '$state': $state
            });
    }));

    it('should define upload component', () => {
        expect(uploadDirective.template).toBe(require('./templates/upload.template'));
        expect(uploadDirective.controller).toBe(UploadComponent);
    });

    it('should upload datacard to service', () => {
        let files = [{ name: 'bob' }]
        setupPostDatacard(files, 'this is a datacard id');
        
        controller.handleFileChanged(files);
        $httpBackend.flush();
    });

    it('should go to data card', () => {
        setupPostDatacard([{}], 'datacardid');
        
        controller.handleFileChanged([{}]);
        $httpBackend.flush();
        expect($state.go).toHaveBeenCalledWith(datacardState, { id: 'datacardid' });
    });

    it('should have datacard name', () => {
        let files = [{ name: 'bob' }];
        setupPostDatacard(files, 'datacardid')

        controller.handleFileChanged(files)
        $httpBackend.flush();
        expect(controller.datacardName).toBe('bob');
    })

    afterEach(() => {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    })

    function setupPostDatacard(files, datacardId) {
        fileReaderFake.binaryString = 'this is a binary string';
        
        let uploadData: UploadData = {
            name: files[0].name,
            file: btoa(fileReaderFake.binaryString)
        };

        $httpBackend.expectPOST('http://localhost:5000/datacards/upload', uploadData)
            .respond(datacardId);
    }
})