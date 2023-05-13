using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Nule.NetStream
{
   /// <summary>
   ///  Serializer contains simple functions that writer/read to a buffered/network stream
   /// </summary>
   public static class Serializer
   {
      /// <summary>
      /// Trys to serialize an unmanaged types.
      /// Returns false if null, returns true otherwise.
      /// </summary>
      public static bool TrySerialize<T>(ref T? type, Span<byte> destination)
         where T : unmanaged
      {
         if (!type.HasValue)
         {
            return false;
         }
         
         T value = type.Value;
         return MemoryMarshal.TryWrite(destination, ref value);
      }
      public static bool TrySerialize<T>(ref T type, Span<byte> destination)
         where T : unmanaged
      {
         return MemoryMarshal.TryWrite(destination, ref type);
      }
      
      public static bool TryDeserialize<T>(Span<byte> source, out T result) where T : unmanaged
      {
         if (source.Length < UnsafeUtility.SizeOf<T>())
         {
            result = default;
            return false;
         }

         return MemoryMarshal.TryRead(source, out result);
      }
   }
}
