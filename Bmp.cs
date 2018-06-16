using System;
using System.IO;

namespace BMPVisualizer
{
    public class Bmp
    {
        public BinaryReader stream;
        public FileHeader fileHeader;
        public ImageHeader imageHeader;

        public uint scanLineWidth;
        public bool scanLineOrder;

        public bool zeroRemainder;

        public uint remainder;
        public uint pad;

        public Bmp(Stream stream)
        {
            this.stream = new BinaryReader(stream);
        }

        public void ReadFileHeader()
        {
            stream.BaseStream.Seek(0, SeekOrigin.Begin);

            fileHeader.signature = stream.ReadChars(2);
            fileHeader.size = stream.ReadUInt32();
            fileHeader.reserved1 = stream.ReadUInt16();
            fileHeader.reserved2 = stream.ReadUInt16();
            fileHeader.offset = stream.ReadUInt32();
        }

        public void ReadImageHeader()
        {
            stream.BaseStream.Seek(14, SeekOrigin.Begin);

            imageHeader.size = stream.ReadUInt32();
            imageHeader.width = stream.ReadUInt32();
            imageHeader.height = stream.ReadInt32();

            imageHeader.planes = stream.ReadInt32();
            imageHeader.bits = stream.ReadUInt16();

            imageHeader.compression = stream.ReadUInt32();
            imageHeader.imageSize = stream.ReadUInt32();

            imageHeader.xresPerMeter = stream.ReadUInt32();
            imageHeader.yresPerMeter = stream.ReadUInt32();

            imageHeader.colorUsed = stream.ReadUInt32();
            imageHeader.significantColors = stream.ReadUInt32();

            remainder = imageHeader.width * 3 % 4;
            pad = 4 - remainder;

            zeroRemainder = remainder == 0;

            scanLineWidth = (zeroRemainder ? imageHeader.width * 3 : imageHeader.width * 3 + 4 - remainder);
            scanLineOrder = imageHeader.height < 0;
        }

        public byte[] ReadAsByteArray()
        {
            SeekToPixelData();

            int size = (int)imageHeader.width * imageHeader.height * 3;
            byte[] buffer = new byte[size];

            int i = 0;

            for (int y = 0; y < Math.Abs(imageHeader.height); y++)
            {
                for (int x = 0; x < imageHeader.width; x++)
                {
                    BmpColor color = ReadPixel();

                    buffer[i * 3] = color.r;
                    buffer[i * 3 + 1] = color.g;
                    buffer[i * 3 + 2] = color.b;

                    i++;
                }

                if (!zeroRemainder) stream.BaseStream.Seek(pad, SeekOrigin.Current);
            }

            return buffer;

        }

        public void SeekToPixelData()
        {
            stream.BaseStream.Seek(fileHeader.offset, SeekOrigin.Begin);
        }

        public BmpColor ReadPixel()
        {
            BmpColor color;

            color.b = stream.ReadByte();
            color.g = stream.ReadByte();
            color.r = stream.ReadByte();

            return color;
        }

        public BmpColor[] ReadScanLine()
        {
            BmpColor[] colors;

            colors = new BmpColor[imageHeader.width];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = ReadPixel();
            }

            if (!zeroRemainder)
            {
                stream.BaseStream.Seek(pad, SeekOrigin.Current);
            }

            return colors;
        }

        public void CloseStream()
        {
            stream.Close();
        }

        //Structs

        public struct FileHeader
        {
            public char[] signature;
            public uint size;
            public ushort reserved1;
            public ushort reserved2;
            public uint offset;
        }

        public struct ImageHeader
        {
            public uint size;

            public uint width;
            public int height;

            public int planes;
            public ushort bits;

            public uint compression;
            public uint imageSize;

            public uint xresPerMeter;
            public uint yresPerMeter;

            public uint colorUsed;
            public uint significantColors;
        }

        public struct BmpColor
        {
            public byte r, g, b;
        }
    }
}
