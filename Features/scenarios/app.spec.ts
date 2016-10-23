import { AppPage } from '../page-objects';

describe('App', () => {
    let appPage: AppPage;

    beforeEach(() => {
        appPage = new AppPage();
        appPage.get();
    });

    it('should say hello', () => {
        expect(appPage.get_title()).toContain('Hello');
    });
});