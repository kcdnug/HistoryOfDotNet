ArrayList customers = new ArrayList();
customers.Add(new Customer("Ada"));
customers.Add(new Order(1234)); // also allowed

Customer customer = (Customer)customers[0];
