
namespace LukeVo.Blazored.Showdown;

public interface IShowdownService : IAsyncDisposable
{
    Task<ShowdownFlavor> GetFlavorAsync();
    Task<T> GetOptionAsync<T>(string key);
    Task InitializeAsync();
    Task SetFlavorAsync(ShowdownFlavor flavor);
    Task SetFlavorAsync(string flavor);
    Task SetOptionAsync(string key, object value);
    Task<string> ToHtmlAsync(string markdown);
    Task<string> ToMarkdownAsync(string html);
}