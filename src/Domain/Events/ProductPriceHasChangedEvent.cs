using Domain.Abstracts;
using Domain.Entities.Products;

namespace Domain.Events;

public sealed record ProductPriceHasChangedEvent(Product Product) : IDomainEvent;
