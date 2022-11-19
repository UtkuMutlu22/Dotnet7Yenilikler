using Dotnet7Yenilikler.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region FixedWindows
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddFixedWindowLimiter("Basic", _option =>
//    {
//        _option.Window = TimeSpan.FromSeconds(12);
//        _option.PermitLimit = 4;
//        _option.QueueLimit = 2;
//        _option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//    });
//});
#endregion

#region SlidingWindows
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddSlidingWindowLimiter("Basic", _option =>
//    {
//        _option.Window = TimeSpan.FromSeconds(12);
//        _option.PermitLimit = 4;
//        _option.QueueLimit = 2;
//        _option.SegmentsPerWindow =2;  
//        _option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//    });
//});
#endregion

#region TokenBucket
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddTokenBucketLimiter("Basic", _options =>
//    {
//        _options.TokenLimit = 4;
//        _options.TokensPerPeriod = 4;
//        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//        _options.QueueLimit = 2;
//        _options.ReplenishmentPeriod = TimeSpan.FromSeconds(12);
//    });
//});
#endregion

#region Concurrency
//builder.Services.AddRateLimiter(options =>
//{
//    options.AddConcurrencyLimiter("Basic", _options =>
//    {
//        _options.PermitLimit = 4;
//        _options.QueueLimit = 2;
//        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//    });
//});
#endregion


#region OnRejected
//builder.Services.AddRateLimiter(options =>
//{

//    options.AddFixedWindowLimiter("Basic", _option =>
//    {
//        _option.Window = TimeSpan.FromSeconds(12);
//        _option.PermitLimit = 4;
//        _option.QueueLimit = 2;
//        _option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//    });
//    options.OnRejected = (Context, CancellationToken) =>
//    {
//        //Log ..

//        return new();
//    };
//});
#endregion

#region Custom

builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy<string, CustomRateLimitPolicy>("CustomPolicy");
});
#endregion
var app = builder.Build();

//app.MapGet("/", () => { }).RequireRateLimiting("...");

app.UseRateLimiter();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
