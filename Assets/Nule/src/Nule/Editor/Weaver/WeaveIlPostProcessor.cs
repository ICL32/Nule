using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Nule.Weaver;
using Unity.CompilationPipeline.Common.ILPostProcessing;

public class WeaveIlPostProcessor : ILPostProcessor
{
    

    public override ILPostProcessor GetInstance() => this;

    public override bool WillProcess(ICompiledAssembly compiledAssembly)
    {
        return compiledAssembly.Name == "Assembly-CSharp";
    }

    public override ILPostProcessResult Process(ICompiledAssembly compiledAssembly)
    {
        WeaveDebugger.ClearLogFile();
        WeaveDebugger.Log("Weave");

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
                ReadSymbols = false,
                SymbolStream = pdbStream,
                AssemblyResolver = assemblyResolver
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

            var codeGenTestType = moduleDefinition.Types.FirstOrDefault(t => t.Name == "CodeGenTest");
            if (codeGenTestType == null)
            {
                WeaveDebugger.Log("Failed to find CodeGenTest type.");
                return null;
            }

            var cheeseCallerMethod =
                new MethodDefinition("CallCheese", MethodAttributes.Public, moduleDefinition.TypeSystem.Void);
            codeGenTestType.Methods.Add(cheeseCallerMethod);

            var loggerType = moduleDefinition.Types.FirstOrDefault(t => t.Name == "Logger");
            if (loggerType == null)
            {
                WeaveDebugger.Log("Failed to find Logger type.");
                return null;
            }

            var cheeseMethod = loggerType.Methods.FirstOrDefault(m => m.Name == "Cheese");
            if (cheeseMethod == null)
            {
                WeaveDebugger.Log("Failed to find Logger.Cheese method.");
                return null;
            }

            var cheeseCallerIlProcessor = cheeseCallerMethod.Body.GetILProcessor();
            cheeseCallerIlProcessor.Append(cheeseCallerIlProcessor.Create(OpCodes.Call,
                moduleDefinition.ImportReference(cheeseMethod)));
            cheeseCallerIlProcessor.Append(cheeseCallerIlProcessor.Create(OpCodes.Ret));

            var startMethod = codeGenTestType.Methods.FirstOrDefault(m => m.Name == "Start");
            if (startMethod != null)
            {
                var processor = startMethod.Body.GetILProcessor();
                var firstInstruction = startMethod.Body.Instructions.First();
                processor.InsertBefore(firstInstruction,
                    processor.Create(OpCodes.Ldarg_0)); // Load the instance of CodeGenTest
                processor.InsertAfter(firstInstruction, processor.Create(OpCodes.Call, cheeseCallerMethod));
            }

            using (var outputStream = new MemoryStream())
            using (var outputPdbStream = new MemoryStream())
            {
                var writerParameters = new WriterParameters
                {
                    WriteSymbols = false,
                    SymbolStream = outputPdbStream
                };
                assemblyDefinition.Write(outputStream, writerParameters);

                return new ILPostProcessResult(new InMemoryAssembly(outputStream.ToArray(),
                    outputPdbStream.ToArray()));
            }
        }
    }

}