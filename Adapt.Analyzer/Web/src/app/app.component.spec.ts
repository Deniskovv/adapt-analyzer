import { TestBed } from '@angular/core/testing';

import { AppComponent } from './app.component';

describe('AppComponent', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            declarations: [ AppComponent ]
        });
    });

    it('should have hello', () => {
        let fixture = TestBed.createComponent(AppComponent);
        expect(fixture.nativeElement.innerText).toContain('Hello')
    });
});