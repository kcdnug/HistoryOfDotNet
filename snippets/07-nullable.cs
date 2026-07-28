public Order CreateOrder(Customer customer, Address? shippingAddress)
{
    ArgumentNullException.ThrowIfNull(customer);

    return shippingAddress is null
        ? Order.Pickup(customer)
        : Order.Ship(customer, shippingAddress);
}
