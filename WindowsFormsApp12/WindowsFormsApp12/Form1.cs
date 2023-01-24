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
using AForge; //kamera açma kütüphanleri
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing; // kodumuzu tarayacak kütüphaneler
using ZXing.Aztec;

namespace WindowsFormsApp12
{
    public partial class Form1 : Form
    {
        FilterInfoCollection webCam; // bilgisayara bağlı bütün kameraları gösteren dizi
        VideoCaptureDevice cam;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webCam = new FilterInfoCollection(FilterCategory.VideoInputDevice);// mecvut kameraları webcame atadım
            foreach(FilterInfo dev in webCam)
            {
                comboBox1.Items.Add(dev.Name);// comboboxlara kameraların ismini ekledik
            }
            comboBox1.SelectedIndex = 0;//kameraları tek sefer de göstersin diye 0 a atadık
        }
        private void cam_Newcam(object sender, NewFrameEventArgs eventArgs) // kameraya okuttuğumuz görüntüyü pictureboxta göstersin diye yazılan fonksiyon
        {
            pictureBox1.Image = ((Bitmap)eventArgs.Frame.Clone());// bitmap kameradan gelen görüntüyü gösterir
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cam = new VideoCaptureDevice(webCam[comboBox1.SelectedIndex].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_Newcam);
            cam.Start();
        }

        private void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true; //görüntü okunıyorsa çalışmaya başlar
            timer1.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(cam!=null) // kameranın içi boş değilse kamerayı çalıştır ve durdur
            {
                if (cam.IsRunning==true)
                {
                    cam.Stop();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 ekle = new Form2(); //form2 ye yönlendirsin diye
            ekle.Show();
            this.Hide();
        }
    }
}
