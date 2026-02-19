using Microsoft.EntityFrameworkCore;
using TestAPIs;

var builder = WebApplication.CreateBuilder(args);

//Add DI - add sevrices
builder.Services.AddDbContext<TestDB>(options =>
    options.UseInMemoryDatabase("TestAPIListDB"));

var app = builder.Build();

//Configure pipeline -UseMethod
app.MapGet("/api/testitems", async (TestDB db) =>
    await db.TestItem.ToListAsync());

app.MapPost("/api/testitems", async (TestDB db, TestModelItem item) =>
{
    db.TestItem.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/api/testitems/{item.Id}", item);
});

app.Run();
