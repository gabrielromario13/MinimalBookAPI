using MinimalBookApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/books", async (DataContext context) =>
    await context.Books.ToListAsync());

app.MapGet("/books/{id:int}", async (DataContext context, int id) =>
    await context.Books.FindAsync(id) is { } book ?
        Results.Ok(book) :
        Results.NotFound("Sorry, book not found. :("));

app.MapPost("/books", async (DataContext context, Book book) =>
{
    context.Books.Add(book);
    await context.SaveChangesAsync();
    return Results.Ok(await context.Books.ToListAsync());
});

app.MapPut("/books/{id:int}", async (DataContext context, Book updatedBook, int id) =>
{
    var book = await context.Books.FindAsync(id);
    if (book is null)
        return Results.NotFound("Sorry, this book doesn't exist.");

    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;
    await context.SaveChangesAsync();

    return Results.Ok(await context.Books.ToListAsync());
});

app.MapDelete("/books/{id:int}", async (DataContext context, int id) =>
{
    var book = await context.Books.FindAsync(id);
    if (book is null)
        return Results.NotFound("Sorry, this book doesn't exist.");

    context.Books.Remove(book);
    await context.SaveChangesAsync();

    return Results.Ok(await context.Books.ToListAsync());
});

app.Run();