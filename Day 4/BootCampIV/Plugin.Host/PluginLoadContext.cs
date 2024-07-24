using System.Reflection;
using System.Runtime.Loader;
using Plugin.Common;

namespace Plugin.Host;

public class PluginLoadContext : AssemblyLoadContext
{
    private AssemblyDependencyResolver _resolver;

    public PluginLoadContext(string pluginPath, bool collectible) : base(name: Path.GetFileName(pluginPath),
        collectible)
    {
        _resolver = new AssemblyDependencyResolver(pluginPath);
    }

    protected override Assembly Load(AssemblyName assemblyName)
    {
        if (assemblyName.Name == typeof(ITextPlugin).Assembly.GetName().Name) return null;
        string target = _resolver.ResolveAssemblyToPath(assemblyName);
        if (target != null)
            return LoadFromAssemblyPath(target);
        return null;
    }
}