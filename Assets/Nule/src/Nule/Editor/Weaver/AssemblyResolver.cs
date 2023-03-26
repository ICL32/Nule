using System;
using System.IO;
using System.Reflection;
using Mono.Cecil;
using UnityEditor;
using UnityEngine;


namespace Nule.Weaver
{
    internal static class AssemblyResolver
    {
        public static DefaultAssemblyResolver Resolver { get; }

        static AssemblyResolver()
        {
            Resolver = new DefaultAssemblyResolver();

            // Get the path to the Unity editor folder
            string editorPath = Path.GetDirectoryName(EditorApplication.applicationPath);
            if (string.IsNullOrEmpty(editorPath))
            {
                Debug.LogError("Failed to get the path to the Unity editor folder.");
                return;
            }

            // Combine the editor path with the relative paths to the UnityEngine assemblies
            string[] unityAssemblies = new string[]
            {
                Path.Combine(editorPath, "Data/Managed/UnityEngine.dll"),
                Path.Combine(editorPath, "Data/Managed/UnityEngine.CoreModule.dll"),
                Path.Combine(editorPath, "Data/Managed/Assembly-CSharp.dll"),
                Path.Combine(Application.dataPath, "../Library/ScriptAssemblies/Assembly-CSharp.dll"),
                @"D:\Unity\Projects\NULE-T\Library\ScriptAssemblies\Assembly-CSharp.dll"
            };

            // Add the search directories for the UnityEngine assemblies
            foreach (string path in unityAssemblies)
            {
                Debug.Log(path);
                Resolver.AddSearchDirectory(Path.GetDirectoryName(path));
            }
        }
    }

}