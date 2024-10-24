using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Core;
using Bogus.DataSets;
using UiS.Dat240.Lab3.SharedKernel;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Events;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace UiS.Dat240.Lab3.Core.Domain.Invoicing.Handlers;

public class InvoiceEventHandler : INotificationHandler<OrderPlaced>{
    private readonly ShopContext _db;
    public InvoiceEventHandler(ShopContext db){
        _db = db;
    }
    public async Task Handle(OrderPlaced notification, CancellationToken cancellationToken){
        var order = await _db.Orders.FindAsync(notification.OrderId);
        if (order == null) throw new System.Exception("Order not found");
        var totalAmount = order.OrderLines.Sum(ol => ol.Price * ol.Amount);
        var customer = await _db.Customer.SingleOrDefaultAsync(c => c.Name == order.Customer.Name);
        if (customer == null){
            var Customer = new Customer(order.Customer.Name);
            var Address2 = new Address(order.Location.Building, order.Location.RoomNumber, order.Location.Notes ?? "");
            var invoice2 = new Invoice(order.Id, Customer, Address2, totalAmount);
            _db.Invoices.Add(invoice2);
        }else{
            var Address = new Address(order.Location.Building, order.Location.RoomNumber, order.Location.Notes ?? "");
            var invoice = new Invoice(order.Id, customer, Address, totalAmount);
            _db.Invoices.Add(invoice);
        }
        

        await _db.SaveChangesAsync(cancellationToken);
    }
}
