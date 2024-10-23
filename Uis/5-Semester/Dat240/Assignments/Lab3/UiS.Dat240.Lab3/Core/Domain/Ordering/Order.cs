using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Core;
using UiS.Dat240.Lab3.Core.Domain.Cart.Pipelines;
using UiS.Dat240.Lab3.Core.Domain.Invoicing;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Events;
using UiS.Dat240.Lab3.SharedKernel;
using UiS.Dat240.Lab3.Core.Domain.Ordering;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering;

public class Order : BaseEntity{
    private Order(){}
    public Order(Location location,  Ordering.Customer customer) {
        Id = Guid.NewGuid();
        Location = location;
        Customer = customer;
        OrderDate = DateTime.UtcNow;
        OrderLines = new List<OrderLine>();
        Status = Status.New;

    }
    public Guid Id { get; protected set;}
    public DateTime OrderDate {get; private set;}
    public Location Location {get; set;}
    public Ordering.Customer Customer {get; private set;}
    public Status Status {get; set;}
    public List<OrderLine> OrderLines {get; private set;} = new();
    public string? Notes {get; private set;}

    public void AddOrderLine(OrderLine orderLine){
        OrderLines.Add(orderLine);
    }
    public void PlacedOrder(Guid orderId){
        Events.Add(new OrderPlaced(orderId));
    }

 
}