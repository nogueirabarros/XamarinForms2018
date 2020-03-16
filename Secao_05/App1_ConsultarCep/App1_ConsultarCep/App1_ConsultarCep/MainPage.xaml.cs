using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App1_ConsultarCep.Servico.Modelo;
using App1_ConsultarCep.Servico;

namespace App1_ConsultarCep
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args) {
            string cep = CEP.Text.Trim();

            if (isValidCep(cep))
            {
                try
                {
                    Endereco end = ViaCepServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        RESULTADO.Text = String.Format("Endereço: {2} de {3} {0}, {1} ", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else {
                        DisplayAlert("ERRO", "Endereço não encontrado para o cep informado: "+cep, "OK");

                    }


                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }

        }

        private bool isValidCep(string cep) {
            bool valido = true;

            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "CEP inválido! O Cep deve conter 8 caracteres.", "OK");
                valido = false;
            }
            int NovoCep = 0;
            if (!int.TryParse(cep, out NovoCep)) {
                DisplayAlert("Erro", "CEP inválido! O Cep deve ser composto apenas por números.", "OK");
                valido = false;
            }

            return valido; 
        }
    }
}
