using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using FinalProject.Common;
using FinalProject.Common.Entities;
using FinalProject.Data;
using FinalProject.PluginServices;
using FinalProject.QueryServices;
using Plugin.Common;


namespace FinalProject;

public static class Program
{

    public static void Main()
    {
        using (IDbContext dbContext = new AppDbContext())
        {
    
            
            QueryServices.QueryServices.InvoiceAggregations(dbContext);
            var resultQuery2 = QueryServices.QueryServices.RandomTracks(dbContext,100);
            var resultQuery3 = QueryServices.QueryServices.TopRevenueTracks(dbContext, 100);
            var resultQuery4 = QueryServices.QueryServices.CustomersWithLowPricedItems(dbContext);
            var resultQuery5 = QueryServices.QueryServices.LoyalCustomers(dbContext, 100);
            var resultQuery6 = QueryServices.QueryServices.EmployeesAndManagers(dbContext);
            var resultQuery7 = QueryServices.QueryServices.AveragePerMonth(dbContext);
            var resultQuery7_2 = QueryServices.QueryServices.AverageMonthly(dbContext);
            var resultQuery8 = QueryServices.QueryServices.MostPopularEachCountry(dbContext, 100);
            var resultQuery9 = QueryServices.QueryServices.TopPopularTracks(dbContext, 100);
            var resultQuery10 = QueryServices.QueryServices.LastInvoiceDetails(dbContext);
            
            Console.WriteLine("Task 1 results are written in some text files inside project bin/Debug/net8.0");
            Console.WriteLine();

            var resultOfWhere = DynamicQueryServices.Where(dbContext.Employees, "EmployeeId", 2, "GreaterThanOrEqual");
            resultOfWhere = DynamicQueryServices.OrderBy(dbContext.Employees, "FirstName", true);

            foreach (var item in resultOfWhere)
            {
                Console.Write("Employee Id : ");
                Console.Write(item.EmployeeId + "  ");
                Console.Write("Name : ");
                Console.Write(item.FirstName + item.LastName  + "  ");
                Console.WriteLine();
            }
            
            Console.WriteLine();
            
            string loyaltyPluginPath =  Directory.GetCurrentDirectory().ToString() + "/../../../../" +
                                        "Plugin.LoyalityPoints\\bin\\Debug\\net8.0\\Plugin.LoyalityPoints.dll";

    
            string seasonalPluginPath = Directory.GetCurrentDirectory().ToString() + "/../../../../" +
                                        "Plugin.SeasonalBonus\\bin\\Debug\\net8.0\\Plugin.SeasonalBonus.dll";

            
            var resultOfLoyaltyList = PluginServices.PluginServices.CalculateScore(dbContext, customerId: 50, loyaltyPluginPath);
            foreach (var item in resultOfLoyaltyList)
            {
                Console.Write(item.Key + " loyalty score ");
                Console.WriteLine(item.Value);
            }
            
            Console.WriteLine();
            
            resultOfLoyaltyList = PluginServices.PluginServices.CalculateScore(dbContext, customerId: 50, seasonalPluginPath);
            foreach (var item in resultOfLoyaltyList)
            {
                Console.Write("Seasonal " + item.Key + "  ");
                Console.WriteLine(item.Value);
                Console.WriteLine();
            }
        }
    }
}