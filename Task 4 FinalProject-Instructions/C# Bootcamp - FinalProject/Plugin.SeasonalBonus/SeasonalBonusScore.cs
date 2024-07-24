using FinalProject.Common;

namespace Plugin.SeasonalBonus;



public class SeasonalBonusScore:Plugin.Common.IScorePlugin
{
    public Dictionary<string, int> CalculateScore(IDbContext context, int customerId)
    {
        var score = context.Invoices
            .Join(context.InvoiceLines, invoice => invoice.InvoiceId, invoiceLine => invoiceLine.InvoiceId,
                (invoice, invoiceLine) => new { invoice = invoice, invoiceLine = invoiceLine })
            .Where(x => x.invoice.CustomerId == customerId)
            .Where(item => item.invoice.InvoiceDate.Month >= 3 && item.invoice.InvoiceDate.Month <= 5)
            .GroupBy(x => x.invoice.InvoiceDate.Year)
            .Count()*50;
        
        var score1 = context.Invoices
            .Join(context.InvoiceLines, invoice => invoice.InvoiceId, invoiceLine => invoiceLine.InvoiceId,
                (invoice, invoiceLine) => new { invoice = invoice, invoiceLine = invoiceLine })
            .Where(x => x.invoice.CustomerId == customerId)
            .Where(item => item.invoice.InvoiceDate.Month >= 3 && item.invoice.InvoiceDate.Month <= 5)
            .GroupBy(x => x.invoice.InvoiceDate.Year);
        
        
        Dictionary<string, int> scoreDict = new  Dictionary<string, int>{{"Score",score}};
        
        return scoreDict;
    }
}