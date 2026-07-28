List<Customer> preferred = new List<Customer>();

foreach (Customer customer in customers)
{
    if (customer.TotalSpend >= 1000)
    {
        preferred.Add(customer);
    }
}
