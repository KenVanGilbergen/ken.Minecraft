using System;
using System.IO;

namespace ken.Minecraft.Nbt.Streams
{
    public class NonReadableStream : MemoryStream
    {
        public override bool CanRead
        {
            get { return false; }
        }


        public override int ReadByte()
        {
            throw new NotSupportedException();
        }


        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }

}
