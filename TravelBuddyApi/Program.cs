using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Models;
using TravelBuddyApi.Repositories.Abstract;
using TravelBuddyApi.Repositories.Concrete;
using TravelBuddyApi.Services.Implementations;
using TravelBuddyApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddDbContext<TravelBuddyContext>(options =>
//     options.UseInMemoryDatabase("TravelBuddyTestDb"));

//Azure Database Connection
builder.Services.AddDbContext<TravelBuddyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripMemberRepository, TripMemberRepository>();
builder.Services.AddScoped<IFriendshipRepository, FriendshipRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IHobbyRepository, HobbyRepository>();

//Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<ITripMemberService, TripMemberService>();
builder.Services.AddScoped<IFriendshipService, FriendshipService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IHobbyService, HobbyService>();
builder.Services.AddScoped<IMatchMakingService, MatchMakingService>();
builder.Services.AddScoped<JwtTokenService>();

//Add CORS service to allow my react frontend to call my backend api
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowViteFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Secret"]!)
            )
        };
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TravelBuddyContext>();
    context.Database.EnsureCreated();

    if (!context.Users.Any())
    {
        context.Users.Add(new User
        {
            FirstName = "Bruce",
            LastName = "Zhang",
            UserName = "bzha414",
            DateOfBirth = new DateTime(2004, 4, 14),
            Gender = "Male",
            Nationality = "China",
            EmailAddress = "brucezhang414@gmail.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("dkfdfPassword12"),
            ProfileInfo = "Siuuu",
            ProfileImageUrl = ""
        });

        context.SaveChanges();
        Console.WriteLine("User object has been successfully created");
    }
}

app.UseHttpsRedirection();

app.UseCors("AllowViteFrontend");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
