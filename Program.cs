using MediCore.Data;
using MediCore.Services.Imlementations;
using MediCore.Services.Interaces;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/// BUILDER SERVERICES  /// BUILDER SERVERICES  /// BUILDER SERVERICES
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddAutoMapper(typeof(Program));
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

