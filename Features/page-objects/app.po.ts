import { browser, element, by } from 'protractor'

export class AppPage {
    get() {
        browser.get('/');
    }

    get_title() {
        return element(by.tagName('h3')).getText();
    }
}