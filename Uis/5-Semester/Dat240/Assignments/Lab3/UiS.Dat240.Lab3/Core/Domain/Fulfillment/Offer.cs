using System;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment;

public class Offer: BaseEntity{
    protected Offer() { }
    public Offer(Guid orderId, Reimbursement reimbursement){
        Id = Guid.NewGuid();
        OrderId = orderId;
        Reimbursement = reimbursement;
        Shipper = null;
    }

    public Guid Id {get; private set;}
    public Guid OrderId {get; private set;}
    public Reimbursement Reimbursement {get; private set;}
    public Shipper? Shipper {get; set;}

    public void ShipperEvent(Guid orderId){
        Events.Add(new OfferShipperSet(orderId));
    }
}