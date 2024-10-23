using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Cart;
using UiS.Dat240.Lab3.Core.Domain.Cart.Pipelines;
using UiS.Dat240.Lab3.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;
using System;
using UiS.Dat240.Lab3.Infrastructure.Data;
using System.Collections.Generic;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using Microsoft.EntityFrameworkCore;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Pipelines;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment;
using UiS.Dat240.Lab3.Core.Domain.Invoicing;


namespace UiS.Dat240.Lab3.Pages;

public class OrderDetailsModel : PageModel{
    private readonly ShopContext _db;
    private readonly IMediator _mediator;
    public OrderDetailsModel(ShopContext db, IMediator mediator){
        _db = db;
        _mediator = mediator;
    }
    public Order Order {get; set;}
    public Offer Offer {get; set;}
    public Reimbursement Reimbursement {get; set;}
    public Shipper Shipper {get; set;}
    public Invoice Invoice{get; set;}
    public async Task OngetAsync(Guid orderId){
        Order = await _db.Orders.Include(o => o.Customer).Include(o=> o.Location).Include(o=> o.OrderLines).SingleOrDefaultAsync(o=>o.Id== orderId);
        Offer = await _db.Offers.Include(o => o.Reimbursement).Include(o => o.Shipper).SingleOrDefaultAsync(o=>o.OrderId == orderId);
        Invoice = await _db.Invoices.Include(o => o.Address).SingleOrDefaultAsync(o => o.OrderId == orderId);
    }
    public async Task<IActionResult> OnPostAsync(Guid orderId, string shipperName){
        await _mediator.Send(new SetShipper.Request(orderId, shipperName));
        return RedirectToPage("OrderOverview");
    }
}