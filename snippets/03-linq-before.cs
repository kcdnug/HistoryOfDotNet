List<Customer> preferred = new();

foreach (Customer customer in customers)
{
    if (customer.TotalSpend >= 1000)
    {
        preferred.Add(customer);
    }
}
