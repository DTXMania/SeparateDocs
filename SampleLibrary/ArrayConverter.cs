using System;
using System.Runtime.InteropServices;

namespace SampleLibrary
{
    /// <summary>
    ///     <para xml:lang="en">Converts an array to an array of a different type.</para>
    ///     <para xml:lang="ja">配列を異なる型の配列に変換します。</para>
    /// </summary>
    public class ArrayConverter
    {
        /// <summary>
        ///     <para xml:lang="en">Converts float[] to byte[].</para>
        ///     <para xml:lang="ja">float[] を byte[] に変換します。</para>
        /// </summary>
        /// <param name="source">
        ///     <para xml:lang="en">An array of floats.</para>
        ///     <para xml:lang="ja">float 配列。</para>
        /// </param>
        /// <returns>
        ///     <para xml:lang="en">A new array of bytes.</para>
        ///     <para xml:lang="ja">新しい byte 配列。</para>
        /// </returns>
        public static byte[] ToByteArray( float[] source )
        {
            var sourceFloatSpan = new Span<float>( source );
            var sourceByteSpan = MemoryMarshal.Cast<float, byte>( sourceFloatSpan );
            return sourceByteSpan.ToArray();
        }
    }
}
