namespace LV.Blazored.Showdown.Demo.Pages;

partial class Home
{

    string html = "", markdown = "# Hello World!";
    string flavor = "original";

    FrozenDictionary<string, PropertyInfo> settingKeys = FrozenDictionary<string, PropertyInfo>.Empty;
    string currSettingKey = "";
    string currSettingValue = "";

    protected override async Task OnInitializedAsync()
    {
        flavor = showdown.GetFlavor().ToString().ToLowerInvariant();
        ListSettings();

        markdown = await http.GetStringAsync("/sample.MD");
        ToHtml();
    }

    void ListSettings()
    {
        settingKeys = typeof(ShowdownConverterOptions)
            .GetProperties()
            .ToFrozenDictionary(q => q.Name[0..1].ToLowerInvariant() + q.Name[1..]);

        currSettingKey = settingKeys.Keys[0];
        RetrieveSetting();
    }

    static readonly MethodInfo GetOptionMethod = typeof(IShowdownServiceInProcess).GetMethod(nameof(IShowdownServiceInProcess.GetOption))
        ?? throw new InvalidOperationException("Could not find GetOption method.");
    void RetrieveSetting()
    {
        var prop = settingKeys[currSettingKey];

        currSettingValue = GetOptionMethod.MakeGenericMethod([prop.PropertyType])
            .Invoke(showdown, [currSettingKey])
            ?.ToString() ?? "";
    }

    void SetSetting()
    {
        var prop = settingKeys[currSettingKey];        
        var convertedValue = Convert.ChangeType(currSettingValue, prop.PropertyType);

        showdown.SetOption(currSettingKey, convertedValue);
    }

    void ToMarkdown()
    {
        markdown = showdown.ToMarkdown(html);
    }

    void ToHtml()
    {
        html = showdown.ToHtml(markdown);
    }

    void SetFlavor() => showdown.SetFlavor(flavor);

}
