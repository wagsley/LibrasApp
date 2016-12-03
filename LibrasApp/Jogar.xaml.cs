using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Media.PlayTo;
using Windows.UI.Core;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LibrasApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Jogar : Page
    {
        public Jogar()
        {
            this.InitializeComponent();
            listaDePalavras();
        }

        //add dispatchr timer

        DispatcherTimer playlistTimer = null;
        PlayToManager playToManager = null;
        CoreDispatcher dispatcher = null;
        List<string> Images = new List<string>();
        string palavra = string.Empty;
        List<string> listaPalavras = new List<string>();
        Random radomListaPalavra = new Random();

        /// <summary>
        /// Lista de palavras
        /// </summary>
        public void listaDePalavras()
        {
            listaPalavras.Add("CARRO");
            listaPalavras.Add("BOLA");
            listaPalavras.Add("MOTO");
            listaPalavras.Add("CAVALO");
            listaPalavras.Add("UVA");
        }

        /// <summary>
        /// Palavra selecionada para o jogo
        /// </summary>
        public void selecionarPalavra()
        {
           
            int index = radomListaPalavra.Next(listaPalavras.Count);

            palavra = listaPalavras[index];
            Images.Clear();
            Images.Add("libras.jpg");
            //adicionar as imagnes na lista
            for (int i = 0; i < palavra.Length; i++)
                Images.Add(palavra[i].ToString() + ".jpg");
            Images.Add("libras.jpg");
        }
        //add cod to on navigated to function
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //add imags thatyou want to show in your slide show
            selecionarPalavra();        
            playlistTimer = new DispatcherTimer();

            //Set the Time span jis me Images chnge hngay like yha 3 Sec bd Image chng hga
            playlistTimer.Interval = new TimeSpan(0, 0, 3);

            playlistTimer.Tick += playlistTimer_Tick;

            playToManager = PlayToManager.GetForCurrentView();
            playToManager.SourceRequested += playToManager_SourceRequested;
            //right click to bitmapimage and reslove
            ImageSource.Source = new BitmapImage(new Uri("ms-appx:///UserControl/" + Images[count]));


        }

        private void playToManager_SourceRequested(PlayToManager sender, PlayToSourceRequestedEventArgs args)
        {
            var deferral = args.SourceRequest.GetDeferral();
            var handler = dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                args.SourceRequest.SetSource(ImageSource.PlayToSource);
                deferral.Complete();
            });
        }
        int count = 0;

        //function of timer tick
        private void playlistTimer_Tick(object sender, object e)
        {
            if (Images != null)
            {
                if (count == Images.Count - 1)
                {
                    count = 0;
                    playlistTimer.Stop();
                }else
                if (count < Images.Count)
                {
                    count++;
                    ImageRotation();
                }
            }
            //if (Images != null)
            //{
            //    if (count == Images.Count - 1)
            //        count = 0;
            //    if (count < Images.Count)
            //    {
            //        count++;
            //        ImageRotation();
            //    }
            //}
        }


        private void ImageRotation()
        {
            ImageSource.Source = new BitmapImage(new Uri("ms-appx:///UserControl/" + Images[count]));
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {            
                if (Images != null)
                {
                selecionarPalavra();
                playlistTimer.Start();
                }
            txtPalavraDigitada.MaxLength = palavra.Length;           
        }

        private void btnVerificar_Click(object sender, RoutedEventArgs e)
        {
            txtBlcResultado.Text = string.Empty;
            for (int i = 0; i < palavra.Length; i++)
            {
                if (txtPalavraDigitada.Text[i].Equals(( palavra[i])))
                {
                    txtBlcResultado.Text += palavra[i];
                }
                else {
                    txtBlcResultado.Text += "X";
                   // < Run Foreground = "#FF268A35" Text = "ltado" />
                }
            }

            if (txtBlcResultado.Text.Contains("X"))
            {
                txtBlcResultadoFinal.Text = "Para errada, veja a palavara correta";
                txtBlcParavaCorreta.Text = palavra;
            }
            else txtBlcResultadoFinal.Text = "Parabéns você acertou";


        }
        

    }
}
