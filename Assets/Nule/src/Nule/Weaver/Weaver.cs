using System;
using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;

namespace Nule.Weaver
{
   internal static partial class Weaver
   {
      private static TypeCache.TypeCollection _networkBehaviourTypes = UnityEditor.TypeCache.GetTypesDerivedFrom<NetworkBehaviour>();

      //WeaveLoop handles going through types
      private static void MainLoop()
      {
         foreach (Type type in _networkBehaviourTypes)
         {
            if (type.Assembly.FullName == "Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null")
            {
               foreach (MethodInfo method in type.GetMethods())
               {
                  var methodName = method.Name;
                  method.GetCustomAttributes()
               }
            }
         }
         
      }

   }
}
