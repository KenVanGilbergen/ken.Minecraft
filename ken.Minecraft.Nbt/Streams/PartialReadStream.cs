using System;
using System.IO;
using JetBrains.Annotations;

namespace ken.Minecraft.Nbt.Streams
{
    public class PartialReadStream : Stream
    {
        readonly byte[] _placeholderBuffer = new byte[1];
        readonly Stream _baseStream;


        public PartialReadStream([NotNull] Stream baseStream)
        {
            if (baseStream == null) throw new ArgumentNullException("baseStream");
            this._baseStream = baseStream;
        }


        public override void Flush()
        {
            _baseStream.Flush();
        }


        public override long Seek(long offset, SeekOrigin origin)
        {
            return _baseStream.Seek(offset, origin);
        }


        public override void SetLength(long value)
        {
            _baseStream.SetLength(value);
        }


        public override int Read(byte[] buffer, int offset, int count)
        {
            int rv = _baseStream.Read(_placeholderBuffer, 0, 1);
            if (rv <= 0)
            {
                return rv;
            }
            else
            {
                buffer[offset] = _placeholderBuffer[0];
                return 1;
            }
        }


        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }


        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return _baseStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { return _baseStream.Length; }
        }

        public override long Position
        {
            get { return _baseStream.Position; }
            set { _baseStream.Position = value; }
        }
    }
}