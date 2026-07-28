var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/orders/{id}", async (int id, IOrderStore store) =>
    await store.GetAsync(id));

app.Run();
