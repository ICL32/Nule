using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Nule.Weaver
{
    //We can't use Debug.Log to debug since it's a native method within Unity
    //To debug we will generate a txt file that contains error logs
    public static class WeaveDebugger
    {
        private const string LogFilePath = "log.txt";
        
        internal static void Log(string message)
        {
            using (var writer = File.AppendText(LogFilePath))
            {
                writer.WriteLine(message);
            }
        }
        
        internal static void ClearLogFile()
        {
            if (File.Exists(LogFilePath))
            {
                File.WriteAllText(LogFilePath, string.Empty);
            }
        }
    }
}
