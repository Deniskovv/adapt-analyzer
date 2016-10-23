import { TestBed, ComponentFixture } from '@angular/core/testing';
import { MaterialModule, MdSidenavLayout } from '@angular/material';

import { AppComponent } from './app.component';

describe('AppComponent', () => {
    let fixture: ComponentFixture<AppComponent>;

    beforeEach(() => {
        fixture = TestBed.createComponent(AppComponent);
    })

    it('should have title', () => {
        expect(fixture.nativeElement.innerText).toContain('Adapt Analyzer');
    });

    it('should have upload button', () => {
        let upload_button = fixture.nativeElement.querySelector('.btn-upload');
        expect(upload_button.disabled).toBeFalsy();
    });
});