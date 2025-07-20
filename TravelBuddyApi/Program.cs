using Microsoft.EntityFrameworkCore;
using TravelBuddyApi.Contexts;
using TravelBuddyApi.Repositories.Abstract;
using TravelBuddyApi.Repositories.Concrete;
using TravelBuddyApi.Services.Implementations;
using TravelBuddyApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TravelBuddyContext>(options =>
    options.UseInMemoryDatabase("TravelBuddyTestDb"));

//Azure Database Connection
/*
builder.Services.AddDbContext<TravelBuddyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
*/

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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
