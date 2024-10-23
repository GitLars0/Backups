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


namespace UiS.Dat240.Lab3.Pages;

public class OrderOverviewModel : PageModel{
    private readonly ShopContext _db;
    public OrderOverviewModel(ShopContext db){
        _db = db;
    }
    public List<Order> Orders {get; set;}

    public async Task OnGetAsync(){
        Orders = await _db.Orders.Include(o => o.Customer).Include(o => o.Location).ToListAsync();
    }
}