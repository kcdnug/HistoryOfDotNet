var preferred = customers
    .Where(c => c.TotalSpend >= 1000)
    .ToList();
