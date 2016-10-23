import { browser, element, by } from 'protractor'

export class AppPage {
    get() {
        browser.get('/');
    }

    is_upload_button_enabled() {
        return element(by.className('btn-upload')).isEnabled();
    }
}