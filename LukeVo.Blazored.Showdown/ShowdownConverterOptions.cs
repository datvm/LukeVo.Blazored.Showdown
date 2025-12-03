namespace LukeVo.Blazored.Showdown;

public class ShowdownConverterOptions
{
    public bool OmitExtraWLInCodeBlocks { get; set; }

    public bool NoHeaderId { get; set; }

    public bool CustomizedHeaderId { get; set; }

    public bool GhCompatibleHeaderId { get; set; }

    /// <summary>
    /// To set to true, 
    /// </summary>
    public string? PrefixHeaderId { get; set; }

    public bool RawPrefixHeaderId { get; set; }

    public bool RawHeaderId { get; set; }

    // Default: 1
    public int HeaderLevelStart { get; set; } = 1;

    public bool ParseImgDimensions { get; set; }

    public bool SimplifiedAutoLink { get; set; }

    public bool LiteralMidWordUnderscores { get; set; }

    public bool Strikethrough { get; set; }

    public bool Tables { get; set; }

    public bool TablesHeaderId { get; set; }

    // Default: true
    public bool GhCodeBlocks { get; set; } = true;

    public bool Tasklists { get; set; }

    public bool SmoothLivePreview { get; set; }

    public bool SmartIndentationFix { get; set; }

    public bool DisableForced4SpacesIndentedSublists { get; set; }

    public bool SimpleLineBreaks { get; set; }

    public bool RequireSpaceBeforeHeadingText { get; set; }

    public bool GhMentions { get; set; }

    // Default: https://github.com/{u}
    public string? GhMentionsLink { get; set; } = "https://github.com/{u}";

    // Default: true
    public bool EncodeEmails { get; set; } = true;

    public bool OpenLinksInNewWindow { get; set; }

    public bool BackslashEscapesHTMLTags { get; set; }

    public bool Emoji { get; set; }

    public bool Underline { get; set; }

    // Default: true
    public bool Ellipsis { get; set; } = true;

    public bool CompleteHtmlDocument { get; set; }

    public bool Metadata { get; set; }

    public bool SplitAdjacentBlockquotes { get; set; }

    public bool MoreStyling { get; set; }
}
