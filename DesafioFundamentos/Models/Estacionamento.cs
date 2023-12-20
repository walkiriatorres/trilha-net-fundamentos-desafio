using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal PrecoInicial = 0;
        private decimal PrecoPorHora = 0;
        private int Vagas = 0;
        private List<string> Veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora, int vagas)
        {
            this.PrecoInicial = precoInicial;
            this.PrecoPorHora = precoPorHora;
            this.Vagas = vagas;
        }

        public void AdicionarVeiculo()
        {
            if(!EstacionamentoEstaLotado()){
                Console.WriteLine("Digite a placa do veículo para estacionar:");
                string placa = Console.ReadLine().ToUpper();
                
                if(ValidarPlaca(placa)) {

                    if (!VeiculoEstaEstacionado(placa)){

                        Veiculos.Add(placa);

                    } else {

                        Console.WriteLine("Veículo já está estacionado.");
                    }

                } else {

                    Console.WriteLine("Placa inválida");                    
                }

            } else {

                Console.WriteLine("Não há vagas disponível no momento.");                               
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para sair:");
            string placa = Console.ReadLine().ToUpper();
            
            if(ValidarPlaca(placa))
            {
                decimal valorAPagar = ConsultarValoraPagar(placa);

                if( valorAPagar > 0)
                {
                    Console.WriteLine("Veículo está estacionado e possui valor a ser pago");
                    Pagar(placa, valorAPagar);

                } else {

                    Console.WriteLine("Veículo NÃO está estacionado. Confira se digitou a placa corretamente");
                }
            } else {

                Console.WriteLine("Placa válida.");

            }
        }

        public void ListarVeiculos()
        {
            if (Veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                
                foreach( string item in Veiculos)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        public bool EstacionamentoEstaLotado()
        {
            bool EstaLotado = false;
            
            if (Veiculos.Count == Vagas)
            {
                EstaLotado = true;
            }

            return EstaLotado;
        }

        public bool VeiculoEstaEstacionado(string placa){
            return Veiculos.Any(x => x.ToUpper() == placa.ToUpper());
        }

        public bool ValidarPlaca(string placa)
        {
            string padraoAnterior = @"^[A-Z]{3}\d{4}$";
            string padraoMercosul = @"^[A-Z]{3}\d{2}[A-Z]{2}$";

            Regex regexAnterior = new Regex(padraoAnterior, RegexOptions.IgnoreCase);
            Regex regexMercosul = new Regex(padraoMercosul, RegexOptions.IgnoreCase);

            return regexAnterior.IsMatch(placa) || regexMercosul.IsMatch(placa);
        }
        public decimal ConsultarValoraPagar(string placa)
        {
            if (VeiculoEstaEstacionado(placa))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                int horas = Convert.ToInt32(Console.ReadLine());
                decimal valorTotal = PrecoInicial + (PrecoPorHora * horas);

                Console.WriteLine($"O valor total a ser pago é: R$ {valorTotal}");

                return valorTotal;
            }
            else
            {
                Console.WriteLine("Não há valores a serem pagos. Veículo não está estacionado.");
                return 0;
            }
        }
 
        public void Pagar(string placa, decimal valorTotal)
        {
            Console.WriteLine("Informe o método de pagamento:");
            Console.WriteLine("1 - Cartão de Débito");
            Console.WriteLine("2 - Cartão de Crédito");
            Console.WriteLine("3 - Dinheiro");
            Console.WriteLine("4 - Pix");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Insira o cartão de débito... Senha solicitada... Processando pagamento... Pagamento recebido! Retire o cartão.");
                    LiberarSaida(placa);
                    break;

                case "2":
                    Console.WriteLine("Insira o cartão de crédito... Senha solicitada... Processando pagamento... Pagamento recebido! Retire o cartão. ");
                    LiberarSaida(placa);
                    break;

                case "3":
                    Console.WriteLine("Recebendo pagamento em dinheiro... Pagamento recebido! ");
                    LiberarSaida(placa);
                    break;

                case "4":
                    Console.WriteLine("Chave Pix telefone: (81)9999-9999");
                    Console.WriteLine("Recebendo pagamento em PIX... Pagamento recebido! ");
                    LiberarSaida(placa);
                    break;

                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }
        public void LiberarSaida(string placa)
        {
            Console.WriteLine($"O veículo placa: {placa} está liberado. Até breve!");
            Veiculos.Remove(placa);
        }
    }
}
