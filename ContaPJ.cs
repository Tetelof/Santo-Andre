using System;

namespace Santo_Andre
{
    public class ContaPJ : Conta
    {

        // Métodos
        public void Pagamento(ContaPF[] ArrayContas)
        // Pagamentos só pode ser efetuados para contas PF
        {
            Console.WriteLine("Digite numero da conta:");
            string conta = Console.ReadLine();
            Console.WriteLine("Digite valor a ser pago:");
            double valor = double.Parse(Console.ReadLine());
            
            saldo = saldo - valor;

            for (int i = 0; i < ArrayContas.Length; i++)
            {
                if (ArrayContas[i].Numero == conta)
                {
                    ArrayContas[i].Deposito(valor);
                }
            }

            Console.WriteLine("Pagamento realizado com sucesso!");
        }
        public void SaquePJ(Notas notas)
        {
            Console.WriteLine("Saldo diponível: "+saldo);
            Console.WriteLine("Digite valor saque:");
            double valor_saque = double.Parse(Console.ReadLine());
            Saque(notas,valor_saque);
        }
    }
}