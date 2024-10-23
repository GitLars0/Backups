using System;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Events;

public record OrderPlaced : BaseDomainEvent {
    public OrderPlaced(Guid orderId){
        OrderId = orderId;
        
    }
    public Guid OrderId {get;}

}