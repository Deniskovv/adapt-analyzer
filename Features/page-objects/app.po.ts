import { browser, element } from 'protractor'

export class AppPage {
    get() {
        browser.get('/');
    }

    get_title() {
        return element('h3').getText();
    }
}