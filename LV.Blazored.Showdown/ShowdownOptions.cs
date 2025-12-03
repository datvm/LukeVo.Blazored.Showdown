namespace LV.Blazored.Showdown;

public class ShowdownOptions
{
    public const string DefaultModuleUrl = "https://cdn.jsdelivr.net/npm/showdown@latest/+esm";

    public string ModuleUrl { get; set; } = DefaultModuleUrl;
}
