// const bool UseCollectibleContext = true;

using System.Reflection;
using Plugin.Common;
using Plugin.Host;

class Program
{
    static void Main()
    {
        const string capitalizer = @"F:\Workspace\BootCampIV\Capitalizer\bin\Debug\net8.0\Capitalizer.dll";
        Console.WriteLine(TransformText("big apple", capitalizer));
    }

    static string TransformText(string text, string pluginPath)
    {
        var alc = new PluginLoadContext(pluginPath, true);
        try
        {
            Assembly ass = alc.LoadFromAssemblyPath(pluginPath);
            Type pluginType = ass.ExportedTypes.Single(t => typeof(ITextPlugin).IsAssignableFrom(t));

            var plugin = (ITextPlugin)Activator.CreateInstance(pluginType);

            return plugin.TransformText(text);
        }
        finally
        {
            alc.Unload();
        }
    }
}
