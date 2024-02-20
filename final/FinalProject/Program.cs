using System;
using System.Collections.Generic;

public class Video
{
    public string Title { get; }
    public string Author { get; }
    public int Length { get; }
    private List<Comment> _comments;

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        _comments = new List<Comment>();
    }

    public void AddComment(string commenterName, string commentText)
    {
        _comments.Add(new Comment(commenterName, commentText));
    }

    public int GetNumComments()
    {
        return _comments.Count;
    }

    public List<Comment> GetAllComments()
    {
        return _comments;
    }
}

public class Comment
{
    public string CommenterName { get; }
    public string CommentText { get; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }
}

public class Product
{
    public string Name { get; }
    public int ProductID { get; }
    public decimal Price { get; }
    public int Quantity { get; }

    public Product(string name, int productId, decimal price, int quantity)
    {
        Name = name;
        ProductID = productId;
        Price = price;
        Quantity = quantity;
    }
}

public class Customer
{
    public string Name { get; }
    public Address Address { get; }

    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }
}

public class Address
{
    public string StreetAddress { get; }
    public string City { get; }
    public string StateProvince { get; }
    public string Country { get; }

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        StreetAddress = streetAddress;
        City = city;
        StateProvince = stateProvince;
        Country = country;
    }
}

public class Order
{
    public Customer Customer { get; }
    private List<Product> _products;

    public Order(Customer customer)
    {
        Customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public decimal CalculateTotalCost()
    {
        decimal totalCost = 0;
        foreach (var product in _products)
        {
            totalCost += product.Price * product.Quantity;
        }
        return totalCost;
    }
}

public abstract class Event
{
    public string EventTitle { get; }
    public string Description { get; }
    public DateTime Date { get; }
    public TimeSpan Time { get; }
    public Address Address { get; }

    public Event(string eventTitle, string description, DateTime date, TimeSpan time, Address address)
    {
        EventTitle = eventTitle;
        Description = description;
        Date = date;
        Time = time;
        Address = address;
    }

    public abstract string GenerateStandardMessage();
}

public class Lecture : Event
{
    public string Speaker { get; }
    public int Capacity { get; }

    public Lecture(string eventTitle, string description, DateTime date, TimeSpan time, Address address, string speaker, int capacity) 
        : base(eventTitle, description, date, time, address)
    {
        Speaker = speaker;
        Capacity = capacity;
    }

    public override string GenerateStandardMessage()
    {
        return $"Event: {EventTitle}, Date: {Date}, Time: {Time}, Location: {Address}";
    }
}

public class Reception : Event
{
    public string RSVP_Email { get; }

    public Reception(string eventTitle, string description, DateTime date, TimeSpan time, Address address, string rsvpEmail) 
        : base(eventTitle, description, date, time, address)
    {
        RSVP_Email = rsvpEmail;
    }

    public override string GenerateStandardMessage()
    {
        return $"Event: {EventTitle}, Date: {Date}, Time: {Time}, Location: {Address}, RSVP Email: {RSVP_Email}";
    }
}

public class OutdoorGathering : Event
{
    public string WeatherForecast { get; }

    public OutdoorGathering(string eventTitle, string description, DateTime date, TimeSpan time, Address address, string weatherForecast) 
        : base(eventTitle, description, date, time, address)
    {
        WeatherForecast = weatherForecast;
    }

    public override string GenerateStandardMessage()
    {
        return $"Event: {EventTitle}, Date: {Date}, Time: {Time}, Location: {Address}, Weather Forecast: {WeatherForecast}";
    }
}

public abstract class Activity
{
    public DateTime Date { get; }
    public int Length { get; }

    public Activity(DateTime date, int length)
    {
        Date = date;
        Length = length;
    }

    public abstract string GetSummary();
}

public class Running : Activity
{
    public double Distance { get; }

    public Running(DateTime date, int length, double distance) : base(date, length)
    {
        Distance = distance;
    }

    public override string GetSummary()
    {
        return $"Ran {Distance} miles in {Length} minutes on {Date}.";
    }
}

public class Cycling : Activity
{
    public double Speed { get; }

    public Cycling(DateTime date, int length, double speed) : base(date, length)
    {
        Speed = speed;
    }

    public override string GetSummary()
    {
        return $"Cycled at {Speed} mph for {Length} minutes on {Date}.";
    }
}

public class Swimming : Activity
{
    public int Laps { get; }

    public Swimming(DateTime date, int length, int laps) : base(date, length)
    {
        Laps = laps;
    }

    public override string GetSummary()
    {
        return $"Swam {Laps} laps in {Length} minutes on {Date}.";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Video video = new Video("Title", "Author", 120);
        video.AddComment("User1", "Great video!");
        video.AddComment("User2", "Nice content!");
        Console.WriteLine($"Number of comments: {video.GetNumComments()}");
        foreach (var comment in video.GetAllComments())
        {
            Console.WriteLine($"{comment.CommenterName}: {comment.CommentText}");
        }
        
        var address = new Address("123 Main St", "City", "State", "Country");
        var customer = new Customer("John Doe", address);
        var order = new Order(customer);
        var product1 = new Product("Product1", 1, 10.0m, 2);
        var product2 = new Product("Product2", 2, 20.0m, 1);
        order.AddProduct(product1);
        order.AddProduct(product2);
        Console.WriteLine($"Total cost: {order.CalculateTotalCost()}");
        
        var lecture = new Lecture("Lecture Title", "Description", DateTime.Now, TimeSpan.FromHours(2), address, "Speaker Name", 100);
        var reception = new Reception("Reception Title", "Description", DateTime.Now, TimeSpan.FromHours(3), address, "example@example.com");
        var outdoorGathering = new OutdoorGathering("Gathering Title", "Description", DateTime.Now, TimeSpan.FromHours(4), address, "Sunny");
        Console.WriteLine(lecture.GenerateStandardMessage());
        Console.WriteLine(reception.GenerateStandardMessage());
        Console.WriteLine(outdoorGathering.GenerateStandardMessage());
        

        var running = new Running(DateTime.Now, 60, 5.0);
        var cycling = new Cycling(DateTime.Now, 45, 15.0);
        var swimming = new Swimming(DateTime.Now, 30, 20);
        Console.WriteLine(running.GetSummary());
        Console.WriteLine(cycling.GetSummary());
        Console.WriteLine(swimming.GetSummary());
    }
}
