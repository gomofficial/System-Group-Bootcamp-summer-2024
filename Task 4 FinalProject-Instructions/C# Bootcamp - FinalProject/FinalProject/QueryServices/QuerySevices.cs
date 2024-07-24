using System.Numerics;
using System.Text;
using FinalProject.Common;
using FinalProject.Common.Entities;
using Microsoft.EntityFrameworkCore;
using FinalProject.Utils;
using Microsoft.Extensions.Primitives;

namespace FinalProject.QueryServices;

public static class QueryServices
{
    // 1. Retrieve the count, sum, and average of the total amounts from invoices using a single query.
    public static void InvoiceAggregations(IDbContext dbContext)
    {
        var result = dbContext.Invoices
            .GroupBy(i => 1)
            .Select(g =>new 
            {
                TotalSum= g.Sum(i => i.Total),
                Count= g.Count(),
                Average = g.Average(i => i.Total)
            })
            .SingleOrDefault();
        var stringList = new List<string>();
        stringList.Add("Average : "+result.Average);
        stringList.Add("Count : " + result.Count);
        stringList.Add("TotalSum : " + result.TotalSum);
        
        FileWriter.WriteList(stringList, "Query 1.txt");
    }
    
    // 2. Fetch a random selection of the top 100 tracks.
    public static List<Track> RandomTracks(IDbContext dbContext, int count)
    {
        var uniqueRandomTracks = dbContext.Tracks.OrderBy(x => EF.Functions.Random())
            .Take(count).ToList();

        
        var stringList = new List<string>();
        foreach (var item in uniqueRandomTracks)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Track : ");
            sb.Append(item.Name + " ");
            sb.Append("TrackId : ");
            sb.Append(item.TrackId);
            stringList.Add(sb.ToString());
        }
        
        FileWriter.WriteList(stringList, "Query 2.txt");
        
        return uniqueRandomTracks;
    }
    
    //  3. Retrieve the top 5 tracks that have the highest sales.
    public static Dictionary<string, double> TopRevenueTracks(IDbContext dbContext,int count)
    {
        // var topTracks = dbContext.InvoiceLines
        //     .Join(dbContext.Tracks, il => il.TrackId, track => track.TrackId, (il, track) => new { il, track })
        //     .GroupBy(g => g.track)
        //     .Select(g => new
        //     {
        //         Track = g.Key,
        //         TotalRevenue = g.Sum(item => item.il.Quantity * item.il.UnitPrice)
        //     })
        //     .OrderByDescending(g => g.TotalRevenue)
        //     .Take(count).ToDictionary(track => track.Track.Name, track => track.TotalRevenue);
        
        var topTracks = dbContext.InvoiceLines
            .GroupBy(g => g.Track)
            .Select(g => new
            {
                Track = g.Key.Name,
                TotalRevenue = g.Sum(item => item.Quantity * item.UnitPrice)
            })
            .OrderByDescending(g => g.TotalRevenue)
            .Take(count).ToDictionary(track => track.Track, track => track.TotalRevenue);

        var stringList = new List<string>();
        foreach (var item in topTracks)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Track : ");
            sb.Append(item.Key + " ");
            sb.Append("Total Revenue : ");
            sb.Append(item.Value);
            stringList.Add(sb.ToString());
        }
        
        FileWriter.WriteList(stringList, "Query 3.txt");
        
        
        return topTracks;
    }
    
    // 4. Obtain all customers with invoices containing at least one line item priced less than $2.
    public static List<Customer> CustomersWithLowPricedItems(IDbContext dbContext)
    {
        var customersWithLowPricedItems = dbContext.Customers
            .Where(c => c.Invoices.Any(i => i.InvoiceLines.Any(ii => ii.UnitPrice < 2)))
            .ToList();
        
        var stringList= new List<string>();
        foreach (var customer in customersWithLowPricedItems)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CustomerID : ");
            sb.Append(customer.CustomerId + " ");
            sb.Append("Customer FullName");
            sb.Append(customer.FirstName + " "+ customer.LastName + " ");
            sb.Append("Email : ");
            sb.Append(customer.Email + " ");
            stringList.Add(sb.ToString());
        }
        
        FileWriter.WriteList(stringList, "Query 4.txt");
        
        return customersWithLowPricedItems;
    }
    
    // 5. Retrieve the top 5 loyal customers based on the total purchase amount across all their invoices
    public static List<Customer> LoyalCustomers(IDbContext dbContext, int count)
    {
        /* var topLoyalCustomers = dbContext.Invoices
             .Join(dbContext.Customers,
                 invoice => invoice.CustomerId,
                 customer => customer.CustomerId,
                 (invoice, customer) => new
                 {
                     Customer = customer,
                     TotalPurchaseAmount = invoice.Total
                 })
             .GroupBy(g => g.Customer)
             .OrderByDescending(g => g.Sum(item => item.TotalPurchaseAmount))
             .Select(g => g.Key)
             .Take(count).ToList(); */
        
        /* var topLoyalCustomers = dbContext.Invoices
             .GroupBy(i => i.Customer)
             .Select(group => new
             {
                 Customer = group.Key,
                 TotalAmount = group.Sum(i => i.Total)
             })
             .OrderByDescending(group => group.TotalAmount)
             .Select(g => g.Customer)
             .Take(count)
             .ToList(); */

        var topLoyalCustomers = dbContext.Customers
            .Select(c => new
            {
                customer = c,
                totalamount = c.Invoices.Sum(i => i.Total)
            })
            .OrderByDescending(item => item.totalamount)
            .Take(count)
            .Select(item => item.customer)
            .ToList();
        
        
        var stringList= new List<string>();
        foreach (var customer in topLoyalCustomers)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CustomerID : ");
            sb.Append(customer.CustomerId + " ");
            sb.Append("Customer FullName");
            sb.Append(customer.FirstName + " "+ customer.LastName + " ");
            sb.Append("Email : ");
            sb.Append(customer.Email + " ");
            stringList.Add(sb.ToString());
        }
        FileWriter.WriteList(stringList, "Query 5.txt");
        
        
        return topLoyalCustomers;
    }
    
    // 6. Retrieve a list of employees along with their respective managers
    public static List<Tuple<Employee, Employee>> EmployeesAndManagers(IDbContext dbContext)
    {
        var employeesWithManagers = (from employee in dbContext.Employees
            join manager in dbContext.Employees
                on employee.ReportsTo equals manager.EmployeeId into managerGroup
            from manager in managerGroup.DefaultIfEmpty()
            select new Tuple<Employee,Employee>(employee, manager)).ToList();
        
        
        var stringList = new List<string>();
        foreach (var item in employeesWithManagers)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("EmployeeId : ");
            sb.Append(item.Item1.EmployeeId);
            sb.Append("Name : ");
            sb.Append(item.Item1.FirstName + item.Item1.LastName);
            sb.Append("Email : ");
            sb.Append(item.Item1.Email);
            if (item.Item2 != null)
            {
                sb.Append("ManagerId : ");
                sb.Append(item.Item2.EmployeeId);
                sb.Append("Manager Name : ");
                sb.Append(item.Item2.FirstName + item.Item2.LastName);
                sb.Append("Manager Email : ");
                sb.Append(item.Item2.Email);
            }
            else
            {
                sb.Append("This Guy Has no Managers!!!");
            }
            stringList.Add(sb.ToString());
        }
        
        FileWriter.WriteList(stringList, "Query 6.txt");
        
        
        return employeesWithManagers;
    } 
    
    // 7. Calculate the average sales per month(Average of Invoices in each month).                
    public static Dictionary<string,double> AveragePerMonth(IDbContext dbContext)
    {
        var averageMonthlySale = (from invoice in dbContext.Invoices
            group invoice by new { Month = invoice.InvoiceDate.Month, Year = invoice.InvoiceDate.Year }
            into monthlyGroup
            select new
            {
                Month = monthlyGroup.Key.Month,
                Year = monthlyGroup.Key.Year,
                AggregatedSales = monthlyGroup.Average(item => item.Total)
            }).OrderBy(result => result.Year).ThenBy(result => result.Month)
            .ToDictionary(result => $"{result.Year}/{result.Month}", result => result.AggregatedSales);
        
        
        var stringList = new List<string>();
        foreach (var item in averageMonthlySale)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Date : ");
            sb.Append(item.Key + " ");
            sb.Append("Mounthly Average : ");
            sb.Append(item.Value);
            stringList.Add(sb.ToString());
        }
        
        FileWriter.WriteList(stringList, "Query 7-1.txt");
        
        return averageMonthlySale;
    }
    
    // 7. Calculate the average sales Monthly((sum of all invoice total prices) / (number of months)
    public static double AverageMonthly(IDbContext dbContext)
    {
        var averageMonthlySale = (from invoice in dbContext.Invoices
            group invoice by new { Month = invoice.InvoiceDate.Month, Year = invoice.InvoiceDate.Year }
            into monthlyGroup
            select new
            {
                Month = monthlyGroup.Key.Month,
                Year = monthlyGroup.Key.Year,
                AggregatedSales = monthlyGroup.Sum(item => item.Total)
            }).OrderBy(result => result.Year).ThenBy(result => result.Month).Average(result => result.AggregatedSales);


        var stringList = new List<string>();
        stringList.Add("Average Monthly : " + averageMonthlySale.ToString());
        FileWriter.WriteList(stringList, "Query7-2.txt");
        
        return averageMonthlySale;
    }

    // 8. Fetch the top 100 most popular genres for each country.
    public static Dictionary<string,List<string>> MostPopularEachCountry(IDbContext dbContext, int count)
    {

        /* var topNPerCountries = dbContext.Invoices.AsEnumerable()
             .Join(dbContext.InvoiceLines, invoice => invoice.InvoiceId, invoiceLine => invoiceLine.InvoiceId,
                 ((invoice, line) => new { invoice, line }))
             .Join(dbContext.Tracks, arg => arg.line.TrackId, track => track.TrackId,
                 (arg, track) => new { arg.invoice, arg.line, track })
             .Join(dbContext.Genres, arg => arg.track.GenreId, genre => genre.GenreId,
                 (arg, genre) => new { arg.invoice, arg.line, arg.track, genre })
             .GroupBy(arg => arg.invoice.BillingCountry)
             .Select(group => new
             {
                 Country = group.Key,
                 TopGenre = group.GroupBy(g => g.genre.Name)
                     .Select(item => new
                     {
                         Genre = item.Key,
                         Popularity = item.Select(item => item.invoice.CustomerId).Distinct().Count()
                     })
                     .OrderByDescending(genre => genre.Popularity)
                     .Take(count).ToDictionary(item => item.Genre, item => item.Popularity)
             }).ToDictionary(item => item.Country, item => item.TopGenre); */
        
        
        /* I have spoke to my Mentor and  Since it was not possible to write this query in SQLITE without
         SQL APPLY which is not available in SQLITE DATABASE method we had to Use As Enumerable which is wrong or
         First get countries and then use countries to get invoices and invoiceLines in each country and then find the most popular Genres in that Country */
        
        var allCountries = dbContext.Invoices.Select(invoice => invoice.BillingCountry).Distinct().ToList();
        var topNPerCountries = new Dictionary<string, List<string>>();
        foreach (var country in allCountries)
        {
            var topNGenre = (from invoice in dbContext.Invoices
                    join invoiceLine in dbContext.InvoiceLines on invoice.InvoiceId equals invoiceLine.InvoiceId
                    join track in dbContext.Tracks on invoiceLine.TrackId equals track.TrackId
                    select new { invoice, invoiceLine, track })
                .Where(item => item.invoice.BillingCountry == country)
                .GroupBy(item => item.track.Genre)
                .Select(g => new
                {
                    Genre = g.Key.Name,
                    Count = g.Select(item => item.invoice.CustomerId).Distinct().Count(),
                    Quantity = g.Select(item => item.invoiceLine.Quantity).Count()
                })
                .OrderByDescending(item => item.Count)
                .ThenByDescending(item => item.Quantity)
                .Take(count).Select(item => item.Genre).ToList();
            topNPerCountries.Add(country,topNGenre);
        }
        
        var stringList= new List<string>();
        foreach (var item in topNPerCountries)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Top {count} Genres for " + item.Key + " : \n");
            foreach (var value in item.Value)
            {
                sb.Append("   " + value + "\n");
            }
            stringList.Add(sb.ToString());
        }

        FileWriter.WriteList(stringList, "Query 8.txt");

        return topNPerCountries;
    }
        
    // 9. Retrieve the top 100 most popular tracks based on customer preferences and purchases.
    public static Dictionary<string,int> TopPopularTracks(IDbContext dbContext, int count)
    {
        var popularTracks = (from invoice in dbContext.Invoices
                join invoiceLine in dbContext.InvoiceLines on invoice.InvoiceId equals invoiceLine.InvoiceId
                group new { invoice, invoiceLine } by invoiceLine.Track)
            .Select(g => new
            {
                Track = g.Key.Name, 
                Popularity = g.Select(g => g.invoice.CustomerId).Distinct().Count(),
                Quantity   = g.Select(g => g.invoiceLine.Quantity).Count()
            })
            .OrderByDescending(item => item.Popularity)
            .ThenByDescending(item => item.Quantity)
            .Take(count).ToDictionary(item => item.Track, item => item.Popularity);
        
        var stringList= new List<string>();
        foreach (var item in popularTracks)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(item.Key);
            sb.Append(item.Value);
            stringList.Add(sb.ToString());
        }

        FileWriter.WriteList(stringList, "Query 9.txt");
        
        return popularTracks;
    }
    
    // 10. Obtain a list of all customers and details of their last invoice, displaying null when a customer has not made any purchases.
    public static Dictionary<int, Invoice> LastInvoiceDetails(IDbContext dbContext)
    {
        var customerLastInvoices = (from customer in dbContext.Customers
            join order in dbContext.Invoices on customer.CustomerId equals order.CustomerId
            group order by customer.CustomerId into customerGroup
            select new
            {
                CustomerID = customerGroup.Key,
                LastOrder = customerGroup.OrderByDescending(o => o.InvoiceDate).FirstOrDefault()
            }).ToDictionary(item => item.CustomerID, item => item.LastOrder);
         
        var stringList= new List<string>();
        foreach (var item in customerLastInvoices)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CustomerId");
            sb.Append(item.Key);
            sb.Append("Last Invoice Info : ");
            sb.Append("InvoiceID : ");
            sb.Append(item.Value.InvoiceId);
            sb.Append("InvoiceDate : ");
            sb.Append(item.Value.InvoiceDate);
            sb.Append("Invoice Total : ");
            sb.Append(item.Value.Total);
            stringList.Add(sb.ToString());
        }
        FileWriter.WriteList(stringList, "Query 10.txt");

        return customerLastInvoices;
    }
}