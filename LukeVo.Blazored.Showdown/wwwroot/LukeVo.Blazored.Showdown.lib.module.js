globalThis.BlazoredShowdown = new class {

    #module;

    async initAsync(modulePath) {
        return this.#module = (await import(modulePath)).default;
    }

    createConverter(options) {
        const converter = new this.#module.Converter(options);
        return converter;
    }

    getModule() {
        return this.#module;
    }

}();