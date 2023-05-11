using System.IO;
using System.Linq;
using Mono.Cecil;
using Nule.Weaver;
using Unity.CompilationPipeline.Common.ILPostProcessing;

public class WeaveILPostProcessor : ILPostProcessor
{


    public override ILPostProcessor GetInstance() => this;

    public override bool WillProcess(ICompiledAssembly compiledAssembly)
    {
        return compiledAssembly.Name == "Assembly-CSharp";
    }

    public override ILPostProcessResult Process(ICompiledAssembly compiledAssembly)
    {
        WeaveDebugger.Log("-------------------------------------------------------------------");
        WeaveDebugger.Log("WeaveIlPostProcessor: Weaving Begun");

        byte[] peData = compiledAssembly.InMemoryAssembly.PeData.ToArray();
        byte[] pdbData = compiledAssembly.InMemoryAssembly.PdbData.ToArray();

        using (var assemblyStream = new MemoryStream(peData))
        using (var pdbStream = new MemoryStream(pdbData))
        {
            var assemblyResolver = new DefaultAssemblyResolver();

            foreach (var reference in compiledAssembly.References)
            {
                if (Path.GetFileName(reference) == "UnityEngine.CoreModule.dll")
                {
                    assemblyResolver.AddSearchDirectory(Path.GetDirectoryName(reference));
                    break;
                }
            }

            var readerParameters = new ReaderParameters
            {
                ReadWrite = true,
                ReadSymbols = true,
                SymbolStream = pdbStream,
                AssemblyResolver = assemblyResolver,
            };

            var assemblyDefinition = AssemblyDefinition.ReadAssembly(assemblyStream, readerParameters);
            if (assemblyDefinition == null)
            {
                WeaveDebugger.Log("Failed to read assembly definition.");
                return null;
            }

            var moduleDefinition = assemblyDefinition.MainModule;
            if (moduleDefinition == null)
            {
                WeaveDebugger.Log("Failed to get main module definition.");
                return null;
            }

            Weaver.EntryPoint(moduleDefinition);

            using (var outputStream = new MemoryStream())
            using (var outputPdbStream = new MemoryStream())
            {
                var writerParameters = new WriterParameters
                {
                    WriteSymbols = false,
                    SymbolStream = outputPdbStream,
                };
                assemblyDefinition.Write("WeaverDebug");

                //Write to in memory assembly
                assemblyDefinition.Write(outputStream, writerParameters);
                InMemoryAssembly memoryAssembly = new InMemoryAssembly(outputStream.ToArray(),
                    outputPdbStream.ToArray());
                return new ILPostProcessResult(memoryAssembly);
            }
        }
    }
}