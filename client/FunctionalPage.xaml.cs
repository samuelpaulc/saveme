using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Save_Me
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FunctionalPage : Page
    {
        public FunctionalPage()
        {
            this.InitializeComponent();
           


        }

        async void OnListenAsync(object sender, RoutedEventArgs e)
        {
            this.recognizer = new SpeechRecognizer();
            await this.recognizer.CompileConstraintsAsync();

            this.recognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(1);
            this.recognizer.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(20);
            this.recognizer.UIOptions.ShowConfirmation = false;
            this.recognizer.UIOptions.IsReadBackEnabled = false;
            this.recognizer.Timeouts.BabbleTimeout = TimeSpan.FromSeconds(0);

            var result = await recognizer.RecognizeWithUIAsync();

            if (result != null)
            {
                StringBuilder builder = new StringBuilder();

                builder.AppendLine(
                  $"I have {result.Confidence} confidence that you said [{result.Text}] " +
                  $"and it took {result.PhraseDuration.TotalSeconds} seconds to say it " +
                  $"starting at {result.PhraseStartTime:g}");

                var alternates = result.GetAlternates(10);

                builder.AppendLine(
                  $"There were {alternates?.Count} alternates - listed below (if any)");

                if (alternates != null)
                {
                    foreach (var alternate in alternates)
                    {
                        builder.AppendLine(
                          $"Alternate {alternate.Confidence} confident you said [{alternate.Text}]");
                    }
                }
                
                if (string.Compare(result.Text, "save me") == 0)
                {
                    MyStackpanel.Visibility = Visibility.Visible;
                    var http = new HttpClient();
                }             
                else
                {
                    MyStackpanel.Visibility = Visibility.Collapsed;
                }
                
            }
        }
        SpeechRecognizer recognizer;


    }
}
