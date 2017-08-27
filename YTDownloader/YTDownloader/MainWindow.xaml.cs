using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;
using System.Diagnostics;
using MessageBox = System.Windows.Forms.MessageBox;
using System.IO;


//To Do:
//Set youtubeDL.exe to custom/temp dir
//Set Config.txt to custom/temp Path
//Set FFMPEG to custom/temp path


namespace YTDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    
    



    public partial class MainWindow : Window
    {

        FolderBrowserDialog SavePath;
        ProcessStartInfo YTDLstartinfo;
        Process YTDLStartInfo2;
        //static void Main(string[] args)
        //{
        //    Trace.Listeners.Clear();

        //    TextWriterTraceListener twtl = new TextWriterTraceListener(Path.Combine(Path.GetTempPath(), AppDomain.CurrentDomain.FriendlyName));
        //    twtl.Name = "TextLogger";
        //    twtl.TraceOutputOptions = TraceOptions.ThreadId | TraceOptions.DateTime;

        //    ConsoleTraceListener ctl = new ConsoleTraceListener(false);
        //    ctl.TraceOutputOptions = TraceOptions.DateTime;

        //    Trace.Listeners.Add(twtl);
        //    Trace.Listeners.Add(ctl);
        //    Trace.AutoFlush = true;

        //    Trace.WriteLine("The first line to be in the logfile and on the console.");
        //}

        public MainWindow()
        {
           // CanvasBorder.BorderThickness = new Thickness(1);

            InitializeComponent();
            SavePath = new FolderBrowserDialog();
            SavePath.RootFolder = Environment.SpecialFolder.MyComputer;
            
            //System.Windows.Forms.Application.StartupPath APPRoot;
        }
        
        public void LocationButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (SavePath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            MessageBox.Show("Music will be downloaded to: " + SavePath.SelectedPath);
            
            InfoStatusBar.Text =  "Save path is: " + SavePath.SelectedPath;
            //MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory + " AppDomain.CurrentDomain.BaseDirectory)");
            //MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly().Location + " System.Reflection.Assembly.GetExecutingAssembly().Location");
            //MessageBox.Show(System.IO.Path.GetDirectoryName(SavePath)); 

        }
        

        public void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            //Build
            //string APPPath = AppDomain.CurrentDomain.BaseDirectory;
            string YTDLSelectedPath = SavePath.SelectedPath;
            string YTDLURL = URLTextBox.Text;
            string YTDLDownloadPath = string.Format(YTDLSelectedPath);
            string YTDLMp3ConfigPath = string.Format(System.Windows.Forms.Application.StartupPath + @"\Data\Mp3Config.txt");
            string YTDLVideoConfigPath = string.Format(System.Windows.Forms.Application.StartupPath + @"\Data\VideoConfig.txt");
            string YTDLAppPath = (System.Windows.Forms.Application.StartupPath + @"\Data\youtube-dl.exe");
            string YTDLFFMpegPath = (System.Windows.Forms.Application.StartupPath + @"\Data\ffmpeg\bin");
            string YTDLRoot = (System.Windows.Forms.Application.StartupPath);
            
            //LocalTesting
            string LocalYTDLMp3ConfigPath = string.Format(@"D:\Programs\YTDL\Mp3Config.txt");
            string LocalYTDLVideoConfigPath = string.Format(@"D:\Programs\YTDL\VideoConfig.txt");
            string LocalYTDLAppPath = (@"D:\Programs\YTDL\youtube-dl.exe");
            string LocalYTDLFFMpegPath = (@"D:\Programs\YTDL");
            //MessageBox.Show(APPPath + " " + YTDLRoot);

            int URLTextboxLength = URLTextBox.Text.Length;
            



            if (Mp3RadioButton.IsChecked == true) {
                if (URLTextboxLength > 1)
                {
                    {
                       if (SavePath.SelectedPath == "")
                        { MessageBox.Show($"No save location selected, downloading to desktop." );
                            YTDLDownloadPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}";
                            MessageBox.Show($"Save path is now {YTDLDownloadPath}");
                        }
                        OptionsStatusBar.Text = "Mp3 is checked.";

                        
                        YTDLstartinfo = new ProcessStartInfo();
                        YTDLStartInfo2 = new Process();

                        
                        YTDLStartInfo2.StartInfo.FileName = LocalYTDLAppPath;
                        YTDLStartInfo2.StartInfo.Arguments = string.Format($@"--config-location {LocalYTDLMp3ConfigPath} -o {YTDLDownloadPath}\%(title)s.%(ext)s --ffmpeg-location {LocalYTDLFFMpegPath} {YTDLURL}");
                        YTDLStartInfo2.StartInfo.UseShellExecute = false;
                        YTDLStartInfo2.StartInfo.RedirectStandardOutput = true;
                        YTDLStartInfo2.Start();
                        Console.WriteLine(YTDLStartInfo2.StandardOutput.ReadToEnd());
                        YTDLStartInfo2.WaitForExit();
                        
                        //string output = YTDLStartInfo2.StandardOutput.ReadToEnd();
                        
                        

                        
                        //YTDLstartinfo.UseShellExecute = false;
                        //YTDLstartinfo.RedirectStandardOutput = true;
                        //YTDLstartinfo.RedirectStandardInput = true;
                        //YTDLstartinfo.FileName = LocalYTDLAppPath;
                        //YTDLstartinfo.Arguments = string.Format($@"--config-location {LocalYTDLMp3ConfigPath} -o {YTDLDownloadPath}\%(title)s.%(ext)s --ffmpeg-location {LocalYTDLFFMpegPath} {YTDLURL}");
                        //YDTLstartinfo.Arguments = ("--config-location " + LocalYTDLMp3ConfigPath + " -o " + YTDLDownloadPath + @"\%(title)s.%(ext)s" + " --ffmpeg-location " + LocalYTDLFFMpegPath + " " + YTDLURL + " > " + YTDLRoot + "\\log.txt");

                        //YTDLstartinfo.Arguments += string.Format($@" cmd /c {YTDLAppPath} > myfile.txt");

                        //Process.Start(YTDLstartinfo);

                        
                        


                        InfoStatusBar.Text = "Finished Downloading";
                    }
                if (URLTextboxLength == 0)
                    {
                        OptionsStatusBar.Text = "Enter URL and try again";

                    }
                }
                
                
            }



            if (VideoRadioButton.IsChecked == true) {

                if (URLTextboxLength < 1)
                {
                    OptionsStatusBar.Text = "";
                    OptionsStatusBar.Text = "Video is checked.";
                    ProcessStartInfo YDTLstartinfo = new ProcessStartInfo();
                    YDTLstartinfo.FileName = YTDLAppPath;
                    YDTLstartinfo.Arguments = ("--config-location " + YTDLVideoConfigPath + " -o " + YTDLDownloadPath + "\\%(title)s.%(ext)s" + " --ffmpeg-location " + YTDLFFMpegPath + " " + YTDLURL + " >> " + "log.txt");
                    Process.Start(YDTLstartinfo);
                    InfoStatusBar.Text = "Finished Downloading";
                }
                if (URLTextboxLength == 0)
                {
                    OptionsStatusBar.Text = "Enter URL and try again";

                }
            };
            OptionsStatusBar.Text = "";
            if (URLTextboxLength == 0)
            {
                OptionsStatusBar.Foreground = Brushes.Red;
                OptionsStatusBar.Text = "Enter URL and try again";
                //Console.Beep(100, 1000);
                //Console.Beep(37, 1000);
                //Console.Beep(1000, 1000);
                System.Media.SystemSounds.Exclamation.Play();
                

            }


            //if (YTDLDownloadPath == APPPath)
            //{
            //    MessageBox.Show("Custom Path not selected, install directory selected");
            //}
            
        }
       
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo YDTLstartinfo = new ProcessStartInfo();
            string YTDLAppPath = (System.Windows.Forms.Application.StartupPath + "\\Data\\youtube-dl.exe");

            YDTLstartinfo.FileName = YTDLAppPath;
            YDTLstartinfo.Arguments = ("-U");
            Process.Start(YDTLstartinfo);


        }

        private void Mp3RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            OptionsStatusBar.Foreground = Brushes.Lavender;
            OptionsStatusBar.Text = "Mp3 is checked.";
        }

        private void VideoRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            OptionsStatusBar.Foreground = Brushes.Lavender;
            OptionsStatusBar.Text = "Video is checked.";
        }
    }
}