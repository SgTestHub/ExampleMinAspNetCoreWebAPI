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

app.MapPut("/api/testitems/{id}", async (int id,TestDB db, TestModelItem item) =>
{
    var existingItem = await db.TestItem.FindAsync(id);
    if (existingItem == null)
    {
        return Results.NotFound();
    }
    
    item.Name = existingItem.Name;
    item.IsComplete = existingItem.IsComplete;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/testitems/{id}", async (int id, TestDB db) =>
{
    if (await db.TestItem.FindAsync(id) is TestModelItem mItem)
    {
        db.TestItem.Remove(mItem);
        await db.SaveChangesAsync();
    }
    return Results.NoContent();
});

app.Run();
