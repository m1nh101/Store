using Domain.Entities.Baskets;

namespace Application.Contracts;

public interface IBasketRepository
{
  Task<Basket> Get(string userId);
  Task Save(Basket basket);
  void RemoveBasket(string userId);
  Task<IEnumerable<Basket>> GetAll();
}
