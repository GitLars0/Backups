
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;
using UiS.Dat240.Lab3.Infrastructure.Data;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Events;
using UiS.Dat240.Lab3.SharedKernel;
using MediatR;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Services;

public class OrderingService : IOrderingService{
    private readonly ShopContext _db;
    private readonly IMediator _mediator;

    public OrderingService(ShopContext db, IMediator mediator){
        _db = db;
        _mediator = mediator;
    }
    public async Task<int> PlaceOrder(Location location, string customerName, OrderLineDto[] orderLines){
        var customer = await _db.Customers.SingleOrDefaultAsync(c => c.Name == customerName);
 

        if (customer == null){
            customer = new Customer(customerName);
        }
        var order = new Order(location, customer);
        foreach(var line in orderLines){
            var orderLine = new OrderLine(line.FoodItemName, line.Price, line.Amount );
            order.AddOrderLine(orderLine);
        }
        order.Status = Status.Placed;
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        order.PlacedOrder(order.Id);
        return order.Id.GetHashCode();
    }
}
