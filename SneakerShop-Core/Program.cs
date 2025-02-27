

using Grpc.Net.Client;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

using var userChannel = GrpcChannel.ForAddress("https://userservice.sneaker.manhvipro.xyz");

//using var userChannel = GrpcChannel.ForAddress("http://localhost:30000");

using var authChannel = GrpcChannel.ForAddress("http://172.24.32.1:30001");

using var productChannel = GrpcChannel.ForAddress("https://productservice.sneaker.manhvipro.xyz");
using var stockChannel = GrpcChannel.ForAddress("https://stockservice.sneaker.manhvipro.xyz");
using var orderChannel = GrpcChannel.ForAddress("https://orderservice.sneaker.manhvipro.xyz");
using var shippingChannel = GrpcChannel.ForAddress("https://shippingservice.sneaker.manhvipro.xyz");
using var invoiceChannel = GrpcChannel.ForAddress("https://invoiceservice.sneaker.manhvipro.xyz");

var userClient = new UserService.User.UserClient(userChannel);
var authClient = new AuthService.Auth.AuthClient(authChannel);
var productClient = new ProductService.Product.ProductClient(productChannel);
var stockClient = new StockService.Stock.StockClient(stockChannel);
var orderClient = new OrderService.Order.OrderClient(orderChannel);
var shippingClient = new ShippingService.Shipping.ShippingClient(shippingChannel);
var invoiceClinet = new InvoiceService.Invoice.InvoiceClient(invoiceChannel);

builder.Services.Add(ServiceDescriptor.Singleton(typeof(UserService.User.UserClient), userClient));
builder.Services.Add(ServiceDescriptor.Singleton(typeof(AuthService.Auth.AuthClient), authClient));
builder.Services.Add(ServiceDescriptor.Singleton(typeof(ProductService.Product.ProductClient), productClient));
builder.Services.Add(ServiceDescriptor.Singleton(typeof(StockService.Stock.StockClient), stockClient));
builder.Services.Add(ServiceDescriptor.Singleton(typeof(OrderService.Order.OrderClient), orderClient));
builder.Services.Add(ServiceDescriptor.Singleton(typeof(ShippingService.Shipping.ShippingClient), shippingClient));
builder.Services.Add(ServiceDescriptor.Singleton(typeof(InvoiceService.Invoice.InvoiceClient), invoiceClinet));

var app = builder.Build();


// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    
}*/
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
