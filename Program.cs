using MediCore.Data;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/// BUILDER SERVERICES  /// BUILDER SERVERICES  /// BUILDER SERVERICES
builder.Services.AddDbContext<DataContext>();

/// BUILDER SERVERICES  /// BUILDER SERVERICES  /// BUILDER SERVERICES
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

