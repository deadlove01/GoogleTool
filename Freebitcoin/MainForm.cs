using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace Freebitcoin
{
    public partial class Form1 : Form
    {

        private string captchaName = "7.png";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() +"\\"+ captchaName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string rootPath = Directory.GetCurrentDirectory() + "\\";
                using (var engine = new TesseractEngine(rootPath + @"tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(rootPath + captchaName))
                    {
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            Console.WriteLine(text.Replace("\n", " "));
                            lblText.Text = text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
        private const int DefaultIterations = 10000;
        public void MemoryLeakDetector(Pix pix, Func<Pix, Pix> op, int iterations = DefaultIterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                using (Pix result = op(pix))
                {
                    
                    //using (Pix binaryPix = grayPix.BinarizeOtsuAdaptiveThreshold(50, 50, 5, 5, 0.1f)) {
                    System.Console.WriteLine("Memory: {0} MB", System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024));
                }

                GC.Collect();
            }
        }
    }
}
