using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BMPVisualizer
{
    public partial class Form1 : Form
    {
        Bitmap bit;
        uint dataOffset;
        uint width;
        int height;
        bool sign = false;
        ushort bitFormat = 0;
        ushort planes = 1;

        Color[] pallete;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadBMP();
        }

        //BMP functionality

        void ReadBMP(string fileName = "1.bmp")
        {
            Random rnd = new Random();

            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                BinaryReader br = new BinaryReader(fs, Encoding.ASCII);

                //Read

                br.BaseStream.Seek(10, SeekOrigin.Begin);

                dataOffset = br.ReadUInt32();

                br.BaseStream.Seek(18, SeekOrigin.Begin);

                width = br.ReadUInt32();
                height = br.ReadInt32();

                planes = br.ReadUInt16();

                bitFormat = br.ReadUInt16();

                sign = (height < 0);

                height = Math.Abs(height);

                bit = new Bitmap((int)width * planes, height);

                //Data reading

                Color color;

                if (bitFormat == 24)
                {
                    br.BaseStream.Seek(dataOffset, SeekOrigin.Begin);

                    uint remainder = (width * 3) % 4;
                    bool zeroRemainder = (remainder == 0);
                    int scanLineOrder;

                    for (int hgh = 0; hgh < height; hgh++)
                    {
                        for (int wdth = 0; wdth < width; wdth++)
                        {
                            byte R, G, B;

                            B = br.ReadByte();
                            G = br.ReadByte();
                            R = br.ReadByte();

                            color = Color.FromArgb(R, G, B);

                            scanLineOrder = (!sign ? height - 1 - hgh : hgh);

                            bit.SetPixel(wdth, scanLineOrder, color);
                        }

                        if (!zeroRemainder)
                        {
                            uint padding = 4 - remainder;
                            br.BaseStream.Seek(padding, SeekOrigin.Current);
                        }
                    }
                }

                if (bitFormat <= 8)
                {
                    br.BaseStream.Seek(dataOffset - (dataOffset - 54), SeekOrigin.Begin);

                    pallete = new Color[(dataOffset - 54) / 4];
                    for (int i = 0; i < pallete.Length; i++)
                    {
                        byte r, g, b;
                        b = br.ReadByte();
                        g = br.ReadByte();
                        r = br.ReadByte();
                        br.ReadByte();

                        pallete[i] = Color.FromArgb(r, g, b);
                    }

                    br.BaseStream.Seek(dataOffset, SeekOrigin.Begin);

                    uint byteCount = (uint)Math.Ceiling((float)width / 8);
                    uint remainder = (byteCount % 4);
                    uint padding = 4 - remainder;
                    bool zeroRemainder = (remainder == 0);
                    int scanLineOrder;

                    for (int hgh = 0; hgh < height; hgh++)
                    {
                        byte[] colors = br.ReadBytes((int)byteCount);

                        int wdth = 0;

                        for (int xx = 0; xx < width; xx++)
                        {
                            wdth = xx / 8;

                            int mask = (1 << bitFormat) - 1;

                            //byte rgb = (byte)( (colors[wdth] & (1 << 7 >> (xx % 8))) >> (7 - (xx % 8)) );
                            byte rgb = ReadBits(colors[wdth], (byte)bitFormat, (byte)(xx % 8));

                            //byte rgb = (byte)((colors[wdth] & ( (mask << (8 - bitFormat)) >> (xx % 8))) >> (7 - (xx % 8)) );

                            color = pallete[rgb];

                            scanLineOrder = (!sign ? height - 1 - hgh : hgh);

                            bit.SetPixel(xx, scanLineOrder, color);
                        }
                        

                        if (!zeroRemainder)
                        {
                            br.BaseStream.Seek(padding, SeekOrigin.Current);
                        }
                    }
                }

                //Close file

                br.Close();
                fs.Close();
            }
            catch (System.IO.IOException e)
            {
                MessageBox.Show("File is in use by: " + e.Source);
            }
        }

        void ReadGarbageAsBMP(string fileName = "garbage.bmp")
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);

            BinaryReader br = new BinaryReader(fs, Encoding.ASCII);

            //Read

            long fileSize = br.BaseStream.Seek(0, SeekOrigin.End);

            br.BaseStream.Seek(0, SeekOrigin.Begin);

            width = (uint)widthNum.Value;
            height = (int)heightNum.Value;

            bitFormat = 24;

            sign = (height < 0);

            height = Math.Abs(height);

            bit = new Bitmap((int)width, height);

            //Data reading

            Color color;

            int scanLineOrder;

            for (int hgh = 0; hgh < height; hgh++)
            {
                for (int wdth = 0; wdth < width; wdth++)
                {
                    byte R, G, B;

                    if (br.BaseStream.Position < fileSize - 3)
                    {
                        R = br.ReadByte();
                        G = br.ReadByte();
                        B = br.ReadByte();

                        color = Color.FromArgb(R, G, B);

                        scanLineOrder = (!sign ? (height - 1) - hgh : hgh);

                        bit.SetPixel(wdth, scanLineOrder, color);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //Close file

            br.Close();
            fs.Close();

            Invalidate();
        }

        byte ReadBits(byte value, byte count, byte offset)
        {
            byte mask = (byte)((Math.Pow(2, count) - 1));
            mask <<= (8 - (count + offset));

            return (byte)((value & (mask)) >> (8 - count - offset));
        }

        //
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (bit != null)
            {
                e.Graphics.TranslateTransform(0, 38);
                e.Graphics.DrawImage(bit, 0, 0);
            }
        }

        //Drag and Drop

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            ReadBMP(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
            Invalidate();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(bit, DragDropEffects.Copy);
        }

        //Garbage

        private void readGarbageButton_Click(object sender, EventArgs e)
        {
            ReadGarbageAsBMP(garbageFileName.Text);
        }

        private void saveGarbageAsImageButton_Click(object sender, EventArgs e)
        {
            bit.Save("image.png");
        }
    }
}
