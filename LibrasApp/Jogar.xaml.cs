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
using System.Threading;
using Windows.UI.Xaml.Media.Imaging;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LibrasApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Jogar : Page
    {
        string palavra = string.Empty;
        public Jogar()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Palavra selecionada para o jogo
        /// </summary>
        public void  selecionarPalavra()
        {
            var listaPalavra = new string[] { "BOLA", "CARRO", "MOTO","CAVALO" };
            var radomListaPalavra = new Random();
            int index = radomListaPalavra.Next(listaPalavra.Length);

             palavra = listaPalavra[index];
        }

        /// <summary>
        /// Nivel de dificuldade do jogo em palavras por segundo
        /// </summary>
        public void nivelPorSegundo(int i)
        {
            imgLetra.Source = new BitmapImage(new Uri("ms-appx:///UserControl/" + Images[i].ToString()));
            playlistTimer.Stop();
            


        }
        private void btnVerificar_Click(object sender, RoutedEventArgs e)
        {
            

            for (int i = 0; i < palavra.Length; i++)
            {
                if (txtPalavraDigitada.Text[i].Equals(palavra[i]))
                {
                    txtbPalavraCorrigida.Text += palavra[i];
                }
                else txtbPalavraCorrigida.Text += "X";
            }
        }
        DispatcherTimer playlistTimer = null;
        List<string> Images = new List<string>();
        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            selecionarPalavra();

            txtPalavraDigitada.MaxLength = palavra.Length;
           
            for (int i = 0; i < palavra.Length; i++)
                Images.Add(palavra[i].ToString() + ".jpg");
       
            playlistTimer = new DispatcherTimer();
            playlistTimer.Interval = new TimeSpan(0, 0, 5);
            
            for (int i = 0; i < palavra.Length; i++)
            {
                nivelPorSegundo(i);
                playlistTimer.Start();
            }
        }

   
        
    }
}
