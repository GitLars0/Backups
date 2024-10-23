using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Events;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Handlers;

public class OrderPlacedHandler : INotificationHandler<OrderPlaced>{
    private readonly ShopContext _db;
    public OrderPlacedHandler(ShopContext db){
        _db = db;
    }
    public async Task Handle(OrderPlaced notification, CancellationToken cancellationToken ){
        var order = await _db.Orders.FindAsync(notification.OrderId);
        if (order == null) throw new System.Exception("Order not found");

        var totalAmount = order.OrderLines.Sum(ol => ol.Price * ol.Amount);
        var reimbursement = new Reimbursement(totalAmount);
        var offer = new Offer(order.Id, reimbursement);
        _db.Offers.Add(offer);
        //_db.Reimbursements.Add(reimbursement);
        await _db.SaveChangesAsync(cancellationToken);
    }
}