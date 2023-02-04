using System;
using UnityEditor;
using UnityEngine;

namespace Nule.Weaver
{
   internal partial class Weaver : MonoBehaviour
   {
      private TypeCache.TypeCollection _monoBehaviourTypes = UnityEditor.TypeCache.GetTypesDerivedFrom<MonoBehaviour>();

      //WeaveLoop handles going through types
      private void WeaveLoop()
      {
         foreach (Type type in _monoBehaviourTypes)
         {
            if (type.Assembly.FullName == "Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null")
            {
               Debug.Log(type.FullName);
            }
         }
      }

   }
}
