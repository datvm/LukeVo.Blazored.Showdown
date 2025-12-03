namespace LukeVo.Blazored.Showdown;

internal class ShowdownService(
    IJSRuntime js,
    IOptions<ShowdownOptions> opts,
    IOptions<ShowdownConverterOptions> converterOpts
) : IShowdownServiceInProcess
{
    const string HelperPrefix = "globalThis.BlazoredShowdown.";

    readonly IJSInProcessRuntime? jsIP = js as IJSInProcessRuntime;

    IJSObjectReference? module;
    IJSInProcessObjectReference? moduleIP;

    IJSObjectReference? converter;
    IJSInProcessObjectReference? converterIP;

    public async Task InitializeAsync()
    {
        if (module is not null)
        {
            return;
        }

        var o = opts.Value;
        var cOpts = converterOpts.Value;

        module = await js.InvokeAsync<IJSObjectReference>($"{HelperPrefix}initAsync", o.ModuleUrl);
        moduleIP = module as IJSInProcessObjectReference;

        converterIP = jsIP?.Invoke<IJSInProcessObjectReference>($"{HelperPrefix}createConverter", cOpts);
        converter = converterIP ?? await js.InvokeAsync<IJSObjectReference>($"{HelperPrefix}createConverter", cOpts);
    }

    public string ToMarkdown(string html)
    {
        ThrowIfNotInitIP();
        return converterIP.Invoke<string>("makeMarkdown", html);
    }

    public async Task<string> ToMarkdownAsync(string html)
    {
        ThrowIfNotInit();
        return await converter.InvokeAsync<string>("makeMarkdown", html);
    }

    public string ToHtml(string markdown)
    {
        ThrowIfNotInitIP();
        return converterIP.Invoke<string>("makeHtml", markdown);
    }

    public async Task<string> ToHtmlAsync(string markdown)
    {
        ThrowIfNotInit();
        return await converter.InvokeAsync<string>("makeHtml", markdown);
    }

    public void SetOption(string key, object value)
    {
        ThrowIfNotInitIP();

        converterIP.InvokeVoid("setOption", key, value);
    }

    public async Task SetOptionAsync(string key, object value)
    {
        ThrowIfNotInit();
        await converter.InvokeVoidAsync("setOption", key, value);
    }

    public T GetOption<T>(string key)
    {
        ThrowIfNotInitIP();
        return converterIP.Invoke<T>("getOption", key);
    }

    public async Task<T> GetOptionAsync<T>(string key)
    {
        ThrowIfNotInit();
        return await converter.InvokeAsync<T>("getOption", key);
    }

    public void SetFlavor(string flavor)
    {
        ThrowIfNotInitIP();
        converterIP.InvokeVoid("setFlavor", flavor);
    }
    public void SetFlavor(ShowdownFlavor flavor) => SetFlavor(flavor.ToString().ToLowerInvariant());

    public async Task SetFlavorAsync(string flavor)
    {
        ThrowIfNotInit();
        await converter.InvokeVoidAsync("setFlavor", flavor);
    }
    public async Task SetFlavorAsync(ShowdownFlavor flavor) => await SetFlavorAsync(flavor.ToString().ToLowerInvariant());

    public ShowdownFlavor GetFlavor()
    {
        ThrowIfNotInitIP();
        var flavorStr = converterIP.Invoke<string>("getFlavor");
        return Enum.Parse<ShowdownFlavor>(flavorStr, true);
    }

    public async Task<ShowdownFlavor> GetFlavorAsync()
    {
        ThrowIfNotInit();
        var flavorStr = await converter.InvokeAsync<string>("getFlavor");
        return Enum.Parse<ShowdownFlavor>(flavorStr, true);
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        if (module is not null)
        {
            await module.DisposeAsync();
        }

        if (converter is not null)
        {
            await converter.DisposeAsync();
        }
    }

    [MemberNotNull(nameof(module))]
    [MemberNotNull(nameof(converter))]
    void ThrowIfNotInit()
    {
        if (module is null || converter is null)
        {
            throw new InvalidOperationException($"{nameof(ShowdownService)} has not been initialized. Call {nameof(InitializeAsync)} first.");
        }
    }

    [MemberNotNull(nameof(jsIP))]
    [MemberNotNull(nameof(moduleIP))]
    [MemberNotNull(nameof(converterIP))]
    void ThrowIfNotInitIP()
    {
        if (jsIP is null)
        {
            throw new InvalidOperationException($"This operation requires {nameof(IJSInProcessRuntime)}.");
        }

        if (moduleIP is null || converterIP is null)
        {
            throw new InvalidOperationException($"{nameof(ShowdownService)} has not been initialized. Call {nameof(InitializeAsync)} first.");
        }
    }
}
