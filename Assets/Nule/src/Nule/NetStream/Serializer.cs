using System;
using System.Buffers;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Nule.NetStream
{
   /// <summary>
   ///  Serializer contains simple functions that writer/read to a buffered/network stream
   /// </summary>
   public static class Serializer
   {
      private static Span<byte> TrySerialize<T>(ref T type)
         where T : unmanaged
      {
         int size = Marshal.SizeOf<T>(type);
         byte[] tempBuffer = ArrayPool<byte>.Shared.Rent(size);
         unsafe
         {
            fixed (void* tempPointer = &type)
            {
               fixed (byte* bufferPointer = tempBuffer)
                  if(UnsafeUtility.IsBlittable(typeof(T)))
                  {
                     UnsafeUtility.MemCpy(bufferPointer, tempPointer, size);
                  }  
               
            }
         }
         return tempBuffer;
      }
   }
}
