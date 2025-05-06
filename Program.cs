using MediCore.Configurations;
using MediCore.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// REGISTER CONFIGURATION CLASSES
var apiBehaviorConfig = new ApiBehaviorConfiguration();
apiBehaviorConfig.ConfigureApiBehavior(builder.Services);

var exceptionHandlerConfig = new ExceptionHandlerConfiguration();

var corsConfig = new CorsConfiguration();
corsConfig.ConfigureCors(builder.Services);

var serviceConfig = new ServiceConfiguration();
serviceConfig.ConfigureApplicationServices(builder.Services);

var authConfig = new AuthenticationConfiguration(builder.Configuration);
authConfig.ConfigureJwtAuthentication(builder.Services);

var hangfireConfig = new HangFireConfiguration(builder.Configuration);
hangfireConfig.ConfigureHangfireServices(builder.Services);

var app = builder.Build();

exceptionHandlerConfig.CustomExceptionHandler(app);   
hangfireConfig.ConfigureHangfirePipeline(app); 

/// UNCOMMENT CODE BELOW TO INITIALIZE DATA
// AddDepartmentsDATA.InitializeData(app);
// AddDoctorsDATA.InitializeData(app);
//AddMedicationsDATA.InitializeData(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();            
app.UseAuthentication();  
app.UseAuthorization();   
app.UseHttpsRedirection();
app.MapControllers();  
app.Run();