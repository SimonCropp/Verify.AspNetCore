﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RazorPageWebApplication.Pages;

[UsesVerify]
public class Tests
{
    [Fact]
    public async Task PageResult()
    {

        var builder = WebApplication.CreateBuilder();

        builder.Services.AddMvcCore();
        builder.Services.AddRazorPages();

        var app = builder.Build();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.MapRazorPages();

        await app.StartAsync();
        var page = new IndexModel();
        var result = page.OnGet();
        var actionContextAccessor = app.Services.GetRequiredService<IActionContextAccessor>();
        //result.ExecuteResultAsync()
        await Verify(result);
    }

    [Fact]
    public Task ChallengeResult()
    {
        var result = new ChallengeResult(
            "scheme",
            new(
                new Dictionary<string, string?>
                {
                    {
                        "key", "value"
                    }
                }));
        return Verify(result);
    }

    [Fact]
    public Task HttpContext()
    {
        var context = new DefaultHttpContext
        {
            Items = new Dictionary<object, object?>
            {
                {
                    "item1", "value1"
                }
            },
            Connection =
            {
                Id = "ConnectionId"
            }
        };
        return Verify(context);
    }

    [Fact]
    public Task EmptyHttpContext() =>
        Verify(new DefaultHttpContext());

    [Fact]
    public Task HeaderDictionary() =>
        Verify(new HeaderDictionary
        {
            {"key", "value"}
        });

    [Fact]
    public Task FileContentResult()
    {
        var result = new FileContentResult("the content"u8.ToArray(), "text/plain");
        return Verify(result);
    }

    [Fact]
    public Task FileStreamResult()
    {
        var result = new FileStreamResult(new MemoryStream("the content"u8.ToArray()), "text/plain");
        return Verify(result);
    }

    [Fact]
    public Task PhysicalFileResult()
    {
        var result = new PhysicalFileResult("target.txt", "text/plain");
        return Verify(result);
    }

    [Fact]
    public Task VirtualFileResult()
    {
        var result = new VirtualFileResult("target.txt", "text/plain");
        return Verify(result);
    }

    #region TestController

    [Fact]
    public async Task ControllerIntegrationTest()
    {
        var builder = WebApplication.CreateBuilder();

        var controllers = builder.Services.AddControllers();
        // custom extension
        controllers.UseSpecificControllers(typeof(FooController));

        await using var app = builder.Build();
        app.MapControllers();

        await app.StartAsync();

        using var client = new HttpClient();
        var result = client.GetStringAsync($"{app.Urls.First()}/Foo");

        await Verify(result);
    }

    [ApiController]
    [Route("[controller]")]
    public class FooController :
        ControllerBase
    {
        [HttpGet]
        public string Get() =>
            "Foo";
    }

    #endregion
}