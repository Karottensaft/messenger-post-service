using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PostMessengerService.Application.Middlewares;
using PostMessengerService.Application.Services;
using PostMessengerService.Domain.Models;
using PostMessengerService.Infrastructure.Data;
using PostMessengerService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
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
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer("TokenKey", cfg =>
    {
        cfg.RequireHttpsMetadata = true;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = false,
            RequireExpirationTime = false,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(PostInformationProfile), typeof(PostChangeProfile), typeof(PostCreationProfile),
    typeof(CommentInformationProfile), typeof(CommentChangeProfile), typeof(CommentCreateProfile),
    typeof(LikeInformationProfile), typeof(LikeCreateProfile));


builder.Services.AddDbContext<PostDbContext>(options =>
    options.UseNpgsql(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserProviderMiddleware, UserProviderMiddleware>();
builder.Services.AddScoped<IPostRepository<PostModel>, PostRepository>();
builder.Services.AddScoped<ICommentRepository<CommentModel>, CommentRepository>();
builder.Services.AddScoped<ILikeRepository<LikeModel>, LikeRepository>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ILikeService, LikeService>();


var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();