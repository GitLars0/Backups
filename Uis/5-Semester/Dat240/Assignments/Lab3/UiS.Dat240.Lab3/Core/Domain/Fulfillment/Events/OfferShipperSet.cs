using System;
using MediatR;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events;

public record OfferShipperSet : BaseDomainEvent{
    public Guid OfferId {get;}
    public OfferShipperSet(Guid offerId){
        OfferId = offerId;
    }
}