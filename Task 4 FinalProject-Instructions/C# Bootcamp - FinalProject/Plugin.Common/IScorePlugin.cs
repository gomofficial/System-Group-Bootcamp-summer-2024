using FinalProject.Common;
using FinalProject.Common.Entities;

namespace Plugin.Common;

public interface IScorePlugin
{
    
    public Dictionary<string, int> CalculateScore(IDbContext context, int customerId);

}