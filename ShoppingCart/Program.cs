using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShoppingCart;
using ShoppingCart.Models;
using ShoppingCart.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<Basket>();
builder.Services.AddScoped<BasketService>();
builder.Services.AddScoped<IDiscount, PercentageDiscount>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

await builder.Build().RunAsync();
