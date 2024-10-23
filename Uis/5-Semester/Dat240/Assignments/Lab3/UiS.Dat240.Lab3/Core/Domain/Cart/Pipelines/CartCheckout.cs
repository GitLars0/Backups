using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Services;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;
using Microsoft.CodeAnalysis;
using UiS.Dat240.Lab3.Core.Domain.Ordering;


namespace UiS.Dat240.Lab3.Core.Domain.Cart.Pipelines;

public class CartCheckout{
    public record Request(Guid CartId, string Building, string RoomNumber, string Notes, string CustomerName) : IRequest<Unit>;
    public record Response(bool Success, string[] Errors);
    public class Handler : IRequestHandler<Request, Unit>{
        private readonly ShopContext _db;
        private readonly IOrderingService _orderingService;

        public Handler(ShopContext db, IOrderingService orderingService){
            _db = db;
            _orderingService = orderingService;
        }
        
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken){
            var cart = await _db.ShoppingCart.Include(c => c.Items).SingleOrDefaultAsync(c => c.Id == request.CartId);
            if (cart == null){
                throw new System.Exception("Cart not found");
            }

            var orderLines = cart.Items.Select(item => new OrderLineDto(item.Sku, item.Name, item.Count, item.Price)).ToArray();
            var location = new Ordering.Location(request.Building, request.RoomNumber, request.Notes);
            var orderId = await _orderingService.PlaceOrder(location, request.CustomerName, orderLines);
            _db.ShoppingCart.Remove(cart);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}