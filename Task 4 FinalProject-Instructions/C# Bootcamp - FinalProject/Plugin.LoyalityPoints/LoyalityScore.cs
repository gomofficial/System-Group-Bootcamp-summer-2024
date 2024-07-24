using FinalProject.Common;

namespace Plugin.LoyalityPoints;

public class LoyalityScore:Plugin.Common.IScorePlugin
{
    public Dictionary<string, int> CalculateScore(IDbContext context, int customerId)
    {
        var trackAmounts = context.Invoices
            .Join(context.InvoiceLines, o => o.InvoiceId, oi => oi.InvoiceId, (o, oi) => new { o, oi })
            .Where(x => x.o.CustomerId == customerId)
            .GroupBy(x => new { Year = x.o.InvoiceDate.Year, Month = x.o.InvoiceDate.Month })
            .Select(g => new 
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                Score = g.Sum(x => x.oi.Quantity)
            })
            .ToList();
    
        Dictionary<string, int> scores = trackAmounts.Select(monthlyTracks =>
        {
            int tracksBought = monthlyTracks.Score;
            int score = (tracksBought == 1) ? 5 : (tracksBought == 2) ? 15 : (tracksBought >= 3) ? 30 : 0;
            return new
            {
                Year = monthlyTracks.Year,
                Month = monthlyTracks.Month,
                Score = score
            };
        }).ToDictionary(item => $"{item.Year}/{item.Month}", item => item.Score);
    
        return scores;
    }
}