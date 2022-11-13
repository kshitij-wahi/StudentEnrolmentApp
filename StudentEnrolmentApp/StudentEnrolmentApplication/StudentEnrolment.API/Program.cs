using StudentEnrolment.Core.Services;
using StudentEnrolment.Core.Services.Interfaces;
using StudentEnrolment.Infrastructure.Services;
using StudentEnrolment.Infrastructure.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy(name: "StudentRegistrationPolicy",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

builder.Services.AddScoped<IFileHandlingService, FileHandlingService>();
builder.Services.AddScoped(typeof(IJsonHandlingService<>), typeof(JsonHandlingService<>));
builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));
builder.Services.AddScoped<IStudentDetailsService, StudentDetailsService>();
builder.Services.AddScoped<ICoursesService, CoursesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("StudentRegistrationPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
