using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

/// <summary>
///  Serializer contains simple functions that writer/read to a buffered/network stream
/// </summary>
public static class Serializer
{
   static void Serialize<T>(T type)
      where T : unmanaged
   {
      //Check if its blittable or not first before doing a memcpy
      if(UnsafeUtility.IsBlittable(typeof(T)))
      {
         
      }
      
      
   }
}
