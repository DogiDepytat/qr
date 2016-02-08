using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace Камера1
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection videoDevices = null;
        private VideoCaptureDevice videoSource = null;
        private FilterInfoCollection VideoCaptureDevices;
        private VideoCaptureDevice FinalVideo;
        Bitmap bmp = null;
        string s = "";
        

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
           {
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
     foreach (FilterInfo VideoCaptureDevice in VideoCaptureDevices)
            {
                comboBox1.Items.Add(VideoCaptureDevice.Name);
            }
            comboBox1.SelectedIndex = 0;
            FinalVideo = new VideoCaptureDevice();

           }
            void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            lock (s)
            { this.pictureBox1.Image = (Image)eventArgs.Frame.Clone(); }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            BarcodeReader qrDecode = new BarcodeReader(); //чтение QR кода         
            try
        { 
            Result text1 = qrDecode.Decode(new Bitmap(pictureBox1.Image as Bitmap)); //декодирование растрового изображения);   
            textBox1.Text = text1.Text;
        }

            catch 
            {              
                MessageBox.Show("Qr-Код отсуствует или не идентифицирован");                
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            videoSource.Stop();
            pictureBox1.Image = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice); // список устройств
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);  // подключения устройства
            videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);  // создание источника видеосигнала 
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            videoSource.Start();
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                videoSource.DisplayPropertyPage(this.Handle);
            }
            catch
            {
                MessageBox.Show("Включите Web-Камеру");
            }
            
        }
    }
    }

