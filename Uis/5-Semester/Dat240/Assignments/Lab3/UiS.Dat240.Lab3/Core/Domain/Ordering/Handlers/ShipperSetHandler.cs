using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using MediatR;
using Microsoft.Extensions.Primitives;
using UiS.Dat240.Lab3.Core.Domain.Cart.Pipelines;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Events;
using UiS.Dat240.Lab3.Infrastructure.Data;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Handlers;

public class ShipperSetHandler : INotificationHandler<OfferShipperSet>{
    private readonly ShopContext _db;
    public ShipperSetHandler(ShopContext db){
        _db = db;
    }
    public async Task Handle(OfferShipperSet notification, CancellationToken cancellationToken ){
        var order = await _db.Orders.FindAsync(notification.OfferId);
        if (order == null) throw new Exception("Cant find order");
        order.Status = Status.Shipped;
        await _db.SaveChangesAsync(cancellationToken);
    }
}