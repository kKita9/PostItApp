using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PeopleApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja po��czenia z baz� danych
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dodanie kontroler�w
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

// Konfiguracja JWT (je�li u�ywasz autoryzacji)
var key = "SuperSecretKey12345SuperSecretKey12345!"; // Klucz u�ywany do podpisywania JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false, // Mo�esz ustawi� na true, je�li chcesz weryfikowa� Issuer
            ValidateAudience = false, // Mo�esz ustawi� na true, je�li chcesz weryfikowa� Audience
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key))
        };
    });

var app = builder.Build();

// Swagger dost�pny tylko w trybie development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "People API v1");
        c.RoutePrefix = string.Empty; // Swagger dost�pny pod g��wnym adresem
    });
}

app.UseHttpsRedirection();

// Middleware do obs�ugi autoryzacji JWT
app.UseAuthentication();
app.UseAuthorization();

// Mapowanie kontroler�w
app.MapControllers();

app.Run();
