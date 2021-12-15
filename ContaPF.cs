using System;
using System.IO;

namespace Santo_Andre
{
    public class ContaPF : Conta
    {
        

        public void SaquePF(Notas notas)
        {
            Console.WriteLine("Saldo diponível: "+saldo);
            Console.WriteLine("Digite valor saque:");
            double valor_saque = double.Parse(Console.ReadLine());

            string[] arrayLinha = new string[3];
            string[] arraydataHoje = Convert.ToString(DateTime.Now).Split(" ");
            double valorSacado = 0;
            string[] arquivo = CriarArquivoTxt();
            
            foreach (string linha in arquivo)
            {
                arrayLinha = linha.Split(' ');
                if (arrayLinha[0] == arraydataHoje[0])
                {
                    if (double.Parse(arrayLinha[2]) < 0)
                    {
                        valorSacado = valorSacado + double.Parse(arrayLinha[2]);
                    }
                }
            }
            
            if (valorSacado + (valor_saque*-1) > -3000)
            {
                // Console.WriteLine("debug: Entrou If");
                Saque(notas,valor_saque);
            }
            else
            {
                Console.WriteLine("Limite de saque diário atingido!");
                Console.WriteLine("\n\n Pressione Enter para voltar");
                Console.ReadLine();
            }
                      
        }
        public void Deposito(double valor)
        {
            saldo += valor;
            Movimentacao("+",valor);
        }
    }
}