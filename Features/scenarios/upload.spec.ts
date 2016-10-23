import { AppPage } from '../page-objects';

describe('App', () => {
    let appPage: AppPage;

    beforeEach(() => {
        appPage = new AppPage();
        appPage.get();
    });

    it('should allow upload', () => {
        expect(appPage.is_upload_button_enabled()).toBeTruthy()
    });
});