using Microsoft.EntityFrameworkCore;
using PostMessengerService.Application.Middlewares;
using PostMessengerService.Application.Services;
using PostMessengerService.Infrastructure.Data;
using PostMessengerService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(PostInformationProfile), typeof(PostChangeProfile), typeof(PostCreationProfile),
    typeof(CommentInformationProfile), typeof(CommentChangeProfile), typeof(CommentCreateProfile),
    typeof(LikeInformationProfile), typeof(LikeCreateProfile));

builder.Services.AddDbContext<PostDbContext>(options =>
    options.UseNpgsql(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<LikeRepository>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<LikeService>();

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

app.MapControllers();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
