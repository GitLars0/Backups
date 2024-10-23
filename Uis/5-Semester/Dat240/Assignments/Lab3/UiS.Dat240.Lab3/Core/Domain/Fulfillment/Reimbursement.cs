using System;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment;

public class Reimbursement{
    public Guid Id {get; private set;}
    public Shipper? Shipper {get; set;}
    public decimal Amount {get; set;}
    public Guid? InvoiceId {get; private set;}

    public Reimbursement( decimal amount){
        Amount = amount;
        Id = Guid.NewGuid();
        Shipper = null;
    }
}