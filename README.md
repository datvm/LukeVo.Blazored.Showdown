Quickly convert Markdown to HTML and vice versa in Blazor using Showdown.js.

# Demo

Demo: https://datvm.github.io/LukeVo.Blazored.Showdown/

![Demo Screenshot](https://raw.githubusercontent.com/datvm/LukeVo.Blazored.Showdown/refs/heads/master/screenshot.jpg)

# Installation

Install the NuGet package via the .NET CLI:

```bash
dotnet add package LukeVo.Blazored.Showdown
```

# Setup

In your `Program.cs`, register the Showdown service:

```cs
builder.Services.AddBlazoredShowdown();
```

You can also configure the module path if you choose to host locally or pin to a specific version:

```cs
builder.Services.AddBlazoredShowdown(
	configure: opt =>
	{
		// The default url is https://cdn.jsdelivr.net/npm/showdown@latest/+esm
		opt.ModuleUrl = "<Module URL>";
	}
);
```

You can now inject `IShowdownService` and `IShowdownServiceInProcess` singletons into your components or services. You must first call `InitializeAsync()`, either at app start or any time before using other methods. Multiple calls are safe and subsequent calls after the first are no-ops.

```cs
var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var showdown = scope.ServiceProvider.GetRequiredService<IShowdownServiceInProcess>();
    await showdown.InitializeAsync();
}

await app.RunAsync();
```

# Usage

See [Demo Project](https://github.com/datvm/LukeVo.Blazored.Showdown/tree/master/LukeVo.Blazored.Showdown.Demo).