namespace Domain.Enums;

public enum ProductState
{
  Renew = 0,
  SoldOut = 1,
  StopSell = 2,
  New = 3
}

public enum OrderState
{
  WaitPayment = 0,
  Canceled = 1,
  Shipping = 2,
  Shipped = 3,
}