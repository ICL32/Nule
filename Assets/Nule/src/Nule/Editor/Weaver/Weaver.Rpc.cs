using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using UnityEngine;

namespace Nule.Weaver
{
    partial class Weaver
    {
        private static void RpcWeave(this MethodDefinition method)
        {
            Debug.Log($"RpcWeave called for method {method.Name}");
            if (method.Body == null)
            {
                Debug.Log("Method body is null");
                return;
            }

            if (method.Module == null)
            {
                Debug.Log("Method module is null");
                return;
            }

            // Get the reference to the UnityEngine.Debug class
            var debugType = method.Module.ImportReference(typeof(UnityEngine.Debug));
            if (debugType == null)
            {
                Debug.Log("Debug class not found");
                return;
            }

            // Get the reference to the Log method with a single string parameter
            var logMethod = method.Module.ImportReference(debugType.Resolve().Methods.FirstOrDefault(m =>
                m.Name == "Log" && m.Parameters.Count == 1 &&
                m.Parameters[0].ParameterType.FullName == typeof(object).FullName));

            // Check if the Log method was found
            if (logMethod == null)
            {
                Debug.Log("Log method not found");
                return;
            }

            ILProcessor weaver = method.Body.GetILProcessor();
            if (weaver == null)
            {
                Debug.Log("ILProcessor is null");
                return;
            }

            weaver.Emit(OpCodes.Ldstr, "Hi");
            weaver.Emit(OpCodes.Call, logMethod);
            weaver.Emit(OpCodes.Ret);
        }

    }
}