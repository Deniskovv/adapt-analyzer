export class FileReaderFake {
    binaryString: string;
    onload: (evt) => void;

    constructor() {

    }


    readAsBinaryString(file: File) {
        this.onload({
            target: {
                result: this.binaryString
            }
        });
    }
}