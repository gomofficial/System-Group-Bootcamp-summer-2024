using System.Linq.Expressions;
using System.Reflection;
using Plugin.Host;

public class Program
{

        const bool UseCollectibleContexts = true;
        static void Main()
        {
            const string capitalizer = @"C:\source\PluginDemo\"
                                       + @"Capitalizer\bin\Debug\netcoreapp3.0\Capitalizer.dll";
            Console.WriteLine (TransformText ("big apple", capitalizer));
        }
        static string TransformText (string text, string pluginPath)
        {
            var alc = new PluginLoadContext (pluginPath, UseCollectibleContexts);
            try
            {
                Assembly assem = alc.LoadFromAssemblyPath (pluginPath);
                // Locate the type in the assembly that implements ITextPlugin:
                Type pluginType = assem.ExportedTypes.Single (t => 
                    typeof (ITextPlugin).IsAssignableFrom (t));
                // Instantiate the ITextPlugin implementation:
                var plugin = (ITextPlugin.ITextPlugin)Activator.CreateInstance (pluginType);
                // Call the TransformText method
                return plugin.TransformText (text);
            }
            finally
            {
                if (UseCollectibleContexts) alc.Unload();    // unload the ALC
            }
            
        }
}
    
    // public static void Main(string[] args)
    // {
    //     ParameterExpression p = Expression.Parameter (typeof (string), "s");
    //     MemberExpression stringLength = Expression.Property (p, "Length"); 
    //     ConstantExpression five = Expression.Constant (5);
    //     
    //     BinaryExpression comparison = Expression.LessThan (stringLength, five);
    //     Expression<Func<string, bool>> lambda
    //         = Expression.Lambda<Func<string, bool>>(comparison, p);
    //     
    //     Func<string, bool> runnable = lambda.Compile(); 
    //
    //     
    //     Console.WriteLine (runnable ("kangaroo"));           
    //     Console.WriteLine (runnable ("dog")); 
    //     
    // }
// }