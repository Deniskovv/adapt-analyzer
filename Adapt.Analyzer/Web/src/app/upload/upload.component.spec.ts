import {
    BaseResponseOptions, 
    Response,
    XHRBackend,
    RequestMethod
} from '@angular/http';

import { TestBed, ComponentFixture } from '@angular/core/testing';
import { 
    MockConnection, 
    MockBackend 
} from '@angular/http/testing';

import { UploadComponent } from './upload.component';

describe('UploadComponent', () => {
    let backend: MockBackend;
    let fixture: ComponentFixture<UploadComponent>;

    beforeEach(() => {
        backend = TestBed.get(XHRBackend)
        fixture = TestBed.createComponent(UploadComponent);
    });

    it('should have upload button', () => {
        let upload_button = fixture.nativeElement.querySelector('.btn-upload');
        expect(upload_button.disabled).toBeFalsy();
    });

    it('should upload datacard to server', (done) => {
        let target = create_file_input();
        backend.connections.subscribe((conn: MockConnection) => {
            let options = new BaseResponseOptions();
            conn.mockRespond(new Response(options))
            expect(conn.request.method).toBe(RequestMethod.Post);
            expect(conn.request.blob()).toEqual(target.files[0]);
            expect(conn.request.url).toBe('/upload');
            done();
        });
        fixture.componentInstance.upload(target);
    });

    function create_file_input() {
        let file = new Blob([''])
        return {
            files: [file]
        }
    }
});