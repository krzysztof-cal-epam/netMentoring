﻿using CartServiceConsoleApp.BLL.Services;
using CartServiceConsoleApp.DAL.Databases;
using CartServiceConsoleApp.DAL.Repositories;
using CartServiceConsoleApp.Entities;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var connection = config["LiteDb:DatabasePath"];

var liteDbCartDatabase = new LiteDbCartDatabase(connection);
var cartRepository = new CartRepository(liteDbCartDatabase);
var cartService = new CartService(cartRepository);


Guid cartId = Guid.NewGuid();

cartService.AddItem(cartId, new CartItem { Id = 1, Name = "Bike", Price = 5999.00m, Quantity = 1 });
cartService.AddItem(cartId, new CartItem { Id = 2, Name = "Handle bars", Price = 29.99m, Quantity = 2 });
cartService.AddItem(cartId, new CartItem { Id = 3, Name = "Chain", Price = 59.99m, Quantity = 1 });

var items = cartService.GetItems(cartId);
Console.WriteLine($"Items in Cart [{items.Count}]:");
foreach (var item in items)
{
    Console.WriteLine($"Item id: {item.Id}, Item name: {item.Name}, Quantity: {item.Quantity}, Price: {item.Price}");
}

cartService.RemoveItem(cartId, 2);

items = cartService.GetItems(cartId);
Console.WriteLine($"Updated Cart [{items.Count}]:");
foreach (var item in items)
{
    Console.WriteLine($"Item id: {item.Id}, Item name: {item.Name}, Quantity: {item.Quantity}, Price: {item.Price}");
}

Console.WriteLine("End");