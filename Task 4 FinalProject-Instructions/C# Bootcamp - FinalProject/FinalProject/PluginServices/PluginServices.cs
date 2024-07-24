using System.Reflection;
using FinalProject.Common;
using Plugin.Common;

namespace FinalProject.PluginServices;

public static class PluginServices
{
    public static Dictionary<string, int> CalculateScore(IDbContext context, int customerId, string pluginPath)
    {
            Assembly assem = Assembly.LoadFile(pluginPath);
            
            Type pluginType = assem.ExportedTypes.Single (t => typeof (IScorePlugin).IsAssignableFrom (t));
            
            var plugin = (IScorePlugin)Activator.CreateInstance (pluginType);

            var result = plugin.CalculateScore(context, 2);
            
            return result;
    }
}