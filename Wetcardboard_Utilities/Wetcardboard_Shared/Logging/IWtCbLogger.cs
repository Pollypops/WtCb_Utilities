using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Wetcardboard_Shared.Logging
{
    public interface IWtCbLogger
    {
        void Log(string message, LogLevel logLevel = LogLevel.Information, Exception? exception = null, int? userId = null, 
            [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = -1);
    }
}
