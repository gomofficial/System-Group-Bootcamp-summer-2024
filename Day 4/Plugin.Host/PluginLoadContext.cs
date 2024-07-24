using System.Reflection;
using System.Runtime.Loader;

namespace Plugin.Host;

class PluginLoadContext : AssemblyLoadContext
{
    private AssemblyDependencyResolver _resolver;

    public PluginLoadContext (string pluginPath, bool collectible)
        // Give it a friendly name to help with debugging:
        : base (name: Path.GetFileName (pluginPath), collectible)
    {
        // Create a resolver to help us find dependencies.
        _resolver = new AssemblyDependencyResolver (pluginPath);
    }
    protected override Assembly Load (AssemblyName assemblyName)
    {
        // See below
        if (assemblyName.Name == typeof (ITextPlugin.ITextPlugin).Assembly.GetName().Name)
            return null;
        string target = _resolver.ResolveAssemblyToPath (assemblyName);
        if (target != null)
            return LoadFromAssemblyPath (target);
        // Could be a BCL assembly. Allow the default context to resolve.
        return null;   
    }
    protected override IntPtr LoadUnmanagedDll (string unmanagedDllName)
    {
        string path = _resolver.ResolveUnmanagedDllToPath (unmanagedDllName);
        return path == null
            ? IntPtr.Zero
            : LoadUnmanagedDllFromPath (path);
    }
}