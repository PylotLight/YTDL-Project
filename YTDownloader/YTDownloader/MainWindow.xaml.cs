using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using MessageBox = System.Windows.Forms.MessageBox;
using System.IO;


//To Do:
//Set youtubeDL.exe to custom/temp dir
//Set Config.txt to custom/temp Path
//Set FFMPEG to custom/temp path
//Add help page with notes i.e Visual C++ 2010 Redistributable Package (x86).

namespace YTDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>






    public partial class MainWindow : Window
    {

        FolderBrowserDialog SavePath;
        //ProcessStartInfo YTDLstartinfo
        Process YTDLStartInfo2;
       

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

            InfoStatusBar.Text = "Save path is: " + SavePath.SelectedPath;
      
        }
       

        public void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
           
            string YTDLSelectedPath = SavePath.SelectedPath;
            string YTDLURL = URLTextBox.Text;
            string YTDLDownloadPath = string.Format(YTDLSelectedPath);
            string YTDLMp3ConfigPath = string.Format(System.Windows.Forms.Application.StartupPath + @"Data\Mp3Config.txt");
            string YTDLVideoConfigPath = string.Format(System.Windows.Forms.Application.StartupPath + @"""\Data\VideoConfig.txt""");
            string YTDLAppPath = (System.Windows.Forms.Application.StartupPath + @"Data\youtube-dl.exe");
            string YTDLFFMpegPath = (System.Windows.Forms.Application.StartupPath + @"Data\ffmpeg\bin");
            string YTDLRoot = (System.Windows.Forms.Application.StartupPath);

            //LocalTesting
            string LocalYTDLMp3ConfigPath = string.Format(@"D:\Programs\YTDL\Mp3Config.txt");
            string LocalYTDLVideoConfigPath = string.Format(@"D:\Programs\YTDL\VideoConfig.txt");
            string LocalYTDLAppPath = (@"D:\Programs\YTDL\youtube-dl.exe");
            string LocalYTDLFFMpegPath = (@"D:\Programs\YTDL");
           

            int URLTextboxLength = URLTextBox.Text.Length;




            if (Mp3RadioButton.IsChecked == true)
            {
                if (URLTextboxLength > 1)
                {
                    {
                        if (SavePath.SelectedPath == "")
                        {
                            MessageBox.Show($"No save location selected, downloading to desktop.");
                            YTDLDownloadPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}";
                            MessageBox.Show($"Save path is now {YTDLDownloadPath}");
                        }
                        OptionsStatusBar.Text = "Mp3 is checked.";


                        //YTDLstartinfo = new ProcessStartInfo();
                        YTDLStartInfo2 = new Process();
                        
                        try
                        {
                            if (InternalCheckBox.IsChecked == true)
                            {
                                YTDLStartInfo2.StartInfo.Arguments = $@"--config-location {LocalYTDLMp3ConfigPath} -o {YTDLDownloadPath}\%(title)s.%(ext)s --ffmpeg-location {LocalYTDLFFMpegPath} {YTDLURL}";
                                YTDLStartInfo2.StartInfo.FileName = LocalYTDLAppPath;
                            }
                            else
                            {
                                YTDLStartInfo2.StartInfo.Arguments = $@"--config-location {YTDLMp3ConfigPath} -o {YTDLDownloadPath}\%(title)s.%(ext)s --ffmpeg-location {YTDLFFMpegPath} {YTDLURL}";
                                YTDLStartInfo2.StartInfo.FileName = YTDLAppPath;
                            }

                            if (!YTDLURL.Contains("youtube.com"))
                            {
                                MessageBox.Show($"Failed, no youtube URL found. Enter a youtube URL and try again.");
                                return;
                            }
                            else
                            {
                                
                                YTDLStartInfo2.StartInfo.CreateNoWindow = true;
                                YTDLStartInfo2.StartInfo.UseShellExecute = false;
                                YTDLStartInfo2.StartInfo.RedirectStandardOutput = true;
                                YTDLStartInfo2.Start();
                                Console.WriteLine(YTDLStartInfo2.StandardOutput.ReadToEnd());
                                YTDLStartInfo2.WaitForExit();

                                
                                InfoStatusBar.Text = "Finished";
                                //OptionsHelpStatusBar.Text = (Exception Error)
                            }
                        }
                        catch (Exception Error) { MessageBox.Show($"Failed with error: {Error.Message}. " +
                            $"Try using the testing checkbox. {LocalYTDLMp3ConfigPath}");
                            LogsTextBlock.Text = (Error.ToString());


                                }

                    }
                    if (URLTextboxLength == 0)
                    {
                        OptionsStatusBar.Text = "Enter URL and try again";

                    }
                }


            }



            if (VideoRadioButton.IsChecked == true)
            {

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
                OptionsStatusBar.Foreground = System.Windows.Media.Brushes.Red;
                OptionsStatusBar.Text = "Enter URL and try again";
                
                System.Media.SystemSounds.Exclamation.Play();


            }


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

            YTDLStartInfo2 = new Process();
            
            string LocalYTDLAppPath = (@"D:\Programs\YTDL\youtube-dl.exe");
            string YTDLAppPath = (System.Windows.Forms.Application.StartupPath + $@"\Data\youtube-dl.exe");

            if (InternalCheckBox.IsChecked == true)
            {
                try {
                    YTDLStartInfo2.StartInfo.FileName = LocalYTDLAppPath;
                    YTDLStartInfo2.StartInfo.Arguments = $@"-U";
                    YTDLStartInfo2.StartInfo.UseShellExecute = false;
                    YTDLStartInfo2.StartInfo.RedirectStandardOutput = true;
                    YTDLStartInfo2.Start();
                    Console.WriteLine(YTDLStartInfo2.StandardOutput.ReadToEnd());
                    YTDLStartInfo2.WaitForExit();
                }
                catch (Exception Error) { MessageBox.Show($"Failed with error: {Error.Message}"); }

            }
            else
            {
                try {
                    YTDLStartInfo2.StartInfo.Arguments = $@"-U";
                    YTDLStartInfo2.StartInfo.FileName = YTDLAppPath;
                    YTDLStartInfo2.StartInfo.UseShellExecute = false;
                    YTDLStartInfo2.StartInfo.RedirectStandardOutput = true;
                    YTDLStartInfo2.Start();
                    Console.WriteLine(YTDLStartInfo2.StandardOutput.ReadToEnd());
                    YTDLStartInfo2.WaitForExit();
                }
                catch (Exception Error) { MessageBox.Show($"Failed with error: {Error.Message}. " +
                            $"Try using the testing checkbox."); }


            }
            

        }
        
        private void Mp3RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            OptionsStatusBar.Foreground = System.Windows.Media.Brushes.Lavender;
            OptionsStatusBar.Text = "Mp3 is checked.";
        }

        private void VideoRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            OptionsStatusBar.Foreground = System.Windows.Media.Brushes.Lavender;
            OptionsStatusBar.Text = "Video is checked.";
        }

        private void PasteClipboardButton_Click(object sender, RoutedEventArgs e)
        {
            URLTextBox.Text = System.Windows.Clipboard.GetText();
        }

        private void CancelHelpButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }




}