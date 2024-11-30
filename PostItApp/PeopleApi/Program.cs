using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PeopleApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja po³¹czenia z baz¹ danych
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dodanie kontrolerów
builder.Services.AddControllers();

// Konfiguracja Swaggera (dokumentacja API)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "People API",
        Version = "v1",
        Description = "API for managing people and friends"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Konfiguracja JWT (jeœli u¿ywasz autoryzacji)
var key = "SuperSecretKey12345SuperSecretKey12345!"; // Klucz u¿ywany do podpisywania JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false, // Mo¿esz ustawiæ na true, jeœli chcesz weryfikowaæ Issuer
            ValidateAudience = false, // Mo¿esz ustawiæ na true, jeœli chcesz weryfikowaæ Audience
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key))
        };
    });

var app = builder.Build();

// Swagger dostêpny tylko w trybie development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "People API v1");
        c.RoutePrefix = string.Empty; // Swagger dostêpny pod g³ównym adresem
    });
}

app.UseHttpsRedirection();

// Middleware do obs³ugi autoryzacji JWT
app.UseAuthentication();
app.UseAuthorization();

// Mapowanie kontrolerów
app.MapControllers();

app.Run();
