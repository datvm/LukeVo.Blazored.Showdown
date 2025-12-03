
namespace LukeVo.Blazored.Showdown;

public interface IShowdownServiceInProcess : IShowdownService
{
    ShowdownFlavor GetFlavor();
    T GetOption<T>(string key);
    void SetFlavor(ShowdownFlavor flavor);
    void SetFlavor(string flavor);
    void SetOption(string key, object value);
    string ToHtml(string markdown);
    string ToMarkdown(string html);
}