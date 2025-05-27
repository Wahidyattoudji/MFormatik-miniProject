using Serilog;
using System.Runtime.CompilerServices;

namespace VisaBOT.Core.Extentions
{
    public static class ExceptionExtentions
    {
        public static void LogError(this Exception ex, [CallerMemberName] string methodName = "",
                                    [CallerFilePath] string filePath = "")
        {
            string className = System.IO.Path.GetFileNameWithoutExtension(filePath);
            Log.Error(ex, $"Error in {className}.{methodName}: {ex.Message}");
        }
    }
}
