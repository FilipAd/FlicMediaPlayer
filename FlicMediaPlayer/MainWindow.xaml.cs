using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sw= System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using swf=System.Windows.Forms;
using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Threading;


namespace FlicMediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool repeat = false;
        public static bool mute = false;
        private readonly DispatcherTimer timer;
        //   string[] paths, files;
        List<string> paths = new List<string>();
        List<string> files = new List<string>();
        public static double vol = 0;
        public MainWindow()
        {
            InitializeComponent();
            Player.Volume = SpeakerSlider.Value;
            vol = SpeakerSlider.Value;
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            
            if (Player.NaturalDuration.HasTimeSpan)
            {
                TimerSlider.Maximum = Player.NaturalDuration.TimeSpan.TotalSeconds;
                lbCurrent.Text = Player.Position.Minutes.ToString("00") + ":" + Player.Position.Seconds.ToString("00");
                lbMusicLength.Text = (Player.NaturalDuration.TimeSpan.TotalMinutes).ToString("00")+ ":" + Player.NaturalDuration.TimeSpan.Seconds.ToString("00");
                TimerSlider.Value = Player.Position.TotalSeconds;
            }
            else
            {
               
                lbCurrent.Text = "00:00";
                lbMusicLength.Text = "00:00";
                if(repeat)
                {
                    Player.Position = TimeSpan.Zero;
                    Player.Play();
                }
                
            }
        }
       
        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Card_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btSpeaker_Click(object sender, RoutedEventArgs e)
        {
            if (!mute)
            {
                mute = true;
                vol = SpeakerSlider.Value;
                SpeakerSlider.Value = SpeakerSlider.Minimum;
                Player.Volume = SpeakerSlider.Minimum;
                btSpeaker.Background = Brushes.Crimson;
            }
            else
            {
                mute = false;
                SpeakerSlider.Value=0.3;
                Player.Volume = 0.3;
                btSpeaker.Background = Brushes.DarkTurquoise;
            }

        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {

            Player.Play();
     
            timer.Start();
        }

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
          
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter= "Audio (*.mp3,*.acc,*wma)|*.acc;*.mp3;*.wma|All Files (*.*)|*.*"; ;
            ofd.Multiselect = true;
            if(ofd.ShowDialog()==true)
            {
                string[] pomfiles = ofd.SafeFileNames;
                for(int i=0;i<ofd.SafeFileNames.Length;i++)
                files.Add(pomfiles[i]);

                string[] pompaths = ofd.FileNames;
                for (int i = 0; i < ofd.FileNames.Length; i++)
                    paths.Add(pompaths[i]);

                for (int i=0;i<pomfiles.Length;i++)
                {
                    listBoxPlayList.Items.Add(pomfiles[i]);
                }
            }
           


          
        }

        private void listBoxPlayList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbSongName.Text = listBoxPlayList.SelectedItem.ToString();
            Player.Source = new Uri(paths.ElementAt(listBoxPlayList.SelectedIndex));
            
            
           
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Player.Pause();
            timer.Stop();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            Player.Stop();
            
            
        }

        private void btnRewind_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxPlayList.SelectedIndex > 0)
            {
                listBoxPlayList.SelectedIndex = listBoxPlayList.SelectedIndex - 1;
            }
        }

        private void SpeakerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Player.Volume = SpeakerSlider.Value;
            vol = SpeakerSlider.Value;
            if(SpeakerSlider.Value==SpeakerSlider.Minimum)
            {
                btSpeaker.Background= Brushes.Crimson;
                SpeakerSlider.Foreground= Brushes.Crimson;
            }
            else
            {
                btSpeaker.Background = Brushes.DarkTurquoise;
                SpeakerSlider.Foreground = Brushes.DarkTurquoise;
            }
        }

        private void TimerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if (TimerSlider.IsMouseCaptureWithin)
            {
                int pos = Convert.ToInt32(TimerSlider.Value);
                Player.Position = new TimeSpan(0, 0, 0, pos, 0);
            }
            if(TimerSlider.Value==TimerSlider.Maximum && repeat)
            {
                Player.Position = TimeSpan.Zero;
                Player.Play();
            }
        }

        private void TimerSlider_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TimerSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TimerSlider_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TimerSlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TimerSlider_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void TimerSlider_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TimerSlider_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnPNext_Click(object sender, RoutedEventArgs e)
        {
            if(listBoxPlayList.SelectedIndex<listBoxPlayList.Items.Count-1)
            {
                listBoxPlayList.SelectedIndex = listBoxPlayList.SelectedIndex + 1;
                lbSongName.Text = listBoxPlayList.SelectedItem.ToString();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnRew_Click(object sender, RoutedEventArgs e)
        {
            if(!repeat)
            {
                btnRew.Foreground = Brushes.Crimson;
                repeat = true;
            }
            else
            {
                btnRew.Foreground = Brushes.White;
                repeat = false;
            }
        }
    }
}
