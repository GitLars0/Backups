using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using UiS.Dat240.Lab3.Infrastructure.Data;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Pipelines;

public class SetShipper{
    public record Request(Guid OrderId, string ShipperName) : IRequest<Unit>;
    public class Handler: IRequestHandler<Request, Unit>{
        private readonly ShopContext _db;
        public Handler(ShopContext db){
            _db = db;
        }
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken){
            var offer = await _db.Offers.Include(o => o.Reimbursement).SingleOrDefaultAsync(o => o.OrderId == request.OrderId);
            if (offer == null) throw new Exception("Offer not found");
            var order = await _db.Orders.FindAsync(request.OrderId);
            if (order == null) throw new Exception("Order not found");
            var shipper = new Shipper(request.ShipperName);
            offer.Shipper = shipper;
            offer.Reimbursement.Shipper = shipper;
            offer.ShipperEvent(request.OrderId);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}