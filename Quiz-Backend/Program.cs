using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Quiz.Contexts;
using Quiz.Models;
using Quiz.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "JobPortal", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    // opt.ExampleFilters();
});

builder.Services.AddControllers().AddJsonOptions(options =>
 {
     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
     options.JsonSerializerOptions.WriteIndented = true;

 });

builder.Services.AddDbContext<QuizContext>(options =>
           options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           );

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

#region Repositories
builder.Services.AddTransient<IRepository<Guid, User>, UserRepository>();
builder.Services.AddTransient<IRepository<Guid, Admin>, AdminRepository>();
builder.Services.AddTransient<IRepository<Guid, Attender>, AttenderRepository>();
builder.Services.AddTransient<IRepository<Guid, Question>, QuestionRepository>();
builder.Services.AddTransient<IRepository<Guid, Answers>, AnswersRepository>();
builder.Services.AddTransient<IRepository<Guid, QuizAttempt>, QuizAttemptRepository>();
builder.Services.AddTransient<IRepository<Guid, Category>, CategoryRepository>();
builder.Services.AddTransient<IRepository<string, QuizData>, QuizDataRepository>();
#endregion

#region  Services
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IAttemptorService, AttenderService>();
builder.Services.AddTransient<IAttemptService, AttemptService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IQuizService, QuizService>();
#endregion

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
