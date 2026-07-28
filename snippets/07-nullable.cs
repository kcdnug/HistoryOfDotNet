public Order CreateOrder(Customer customer, Address? shippingAddress)
{
    if (customer is null)
        throw new ArgumentNullException(nameof(customer));

    return shippingAddress is null
        ? Order.Pickup(customer)
        : Order.Ship(customer, shippingAddress);
}
