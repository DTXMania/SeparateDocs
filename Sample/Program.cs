using System;

namespace Sample
{
    class Program
    {
        static void Main( string[] args )
        {
            var floats = new float[] { 1f, 2f, 3f };
            var bytes = SampleLibrary.ArrayConverter.ToByteArray( floats );
        }
    }
}
