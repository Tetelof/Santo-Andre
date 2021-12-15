using System;
using System.IO;

namespace Santo_Andre
{
    public class Conta
    {
    // Atributos
        protected string numero;
        protected int senha;
        protected string nome;
        protected double saldo;
        protected bool usuarioBloqueado = false;
        

        // GetSet
        public string Numero
        {
            get{return numero;}
            set{numero = value;}
        }
        public string Nome
        {
            get{return nome;}
            set{nome = value;}
        }
        public double Saldo
        {
            get{return saldo;}
            set{saldo = value;}
        }
        public int Senha
        {
            get{return senha;}
            set{senha = value;}
        }
        public bool UsuarioBloqueado
        {
            get { return usuarioBloqueado; }
            set { usuarioBloqueado = value; }
        }
        
        
        

        // Construtor
        public Conta()
        {
            
        }
        
        // Métodos

        public bool verificarSenha()
        {
            int tentativas = 0;
            while (tentativas <3)
            {
                Console.WriteLine("Digite a senha:");
                int senha = int.Parse(Console.ReadLine());
                if (senha == this.senha)
                {
                    return true;
                }
                else if (tentativas >= 2)
                {
                    Console.WriteLine("Usuario Bloqueado!");
                    usuarioBloqueado = true;
                    return false;
                }
                else
                {
                    Console.WriteLine("Senha incorreta.");
                    tentativas++;
                }
            }
            return false;
        }
        public void AlterarSenha()
        {
            bool senha_ok = false;
            while (!senha_ok)
            {
                Console.WriteLine("Digite nova senha (somente números):");
                int nova_senha = int.Parse(Console.ReadLine());
                Console.WriteLine("Confirme senha:");
                int confirma_senha = int.Parse(Console.ReadLine());
                
                if (nova_senha == this.senha)
                {
                    Console.WriteLine("Nova senha não pode ser igual a anterior.");
                }
                else if (nova_senha == confirma_senha)
                {
                    Console.WriteLine("Senha alterada com sucesso!");
                    this.senha = nova_senha;
                    senha_ok = true;
                }
                else
                {
                    Console.WriteLine("Valores não concidem.");
                }
            }
            Console.WriteLine("\n\n Pressione Enter para voltar");
            Console.ReadLine();
        }
        public string[] CriarArquivoTxt()
        {
            if(!File.Exists(numero+".txt"))
            {
                FileStream f = File.Create(numero+".txt");
                f.Close();
            }
            string[] arquivo = File.ReadAllLines(numero+".txt");
            return arquivo;
        }
        public void Extrato()
        {
            string[] arquivo = CriarArquivoTxt();
            
            foreach (string linha in arquivo)
            {
                Console.WriteLine(linha);
            }
            Console.WriteLine("Saldo atual =" + saldo);
            Console.WriteLine("\n\n Pressione Enter para voltar");
            Console.ReadLine();
        }
        public void Saque(Notas notas, double valor_saque)
        {
            if (valor_saque <= saldo)
            {
                valor_saque = notas.validarValor(valor_saque);
                saldo -= valor_saque;
                Movimentacao("-",valor_saque);
                notas.distribuirNotas(Convert.ToInt32(valor_saque));
            }
            else
            {
                Console.WriteLine("Saldo Insuficiente!");
            }
        }
        public void Deposito()
        {
            Console.WriteLine("Digite o valor a ser depositado:");
            double valor_deposito = double.Parse(Console.ReadLine());
            saldo += valor_deposito;
            Movimentacao("+",valor_deposito);
            Console.WriteLine("Deposito efetuado com sucesso!");
            Console.WriteLine("\n\n Pressione Enter para voltar");
            Console.ReadLine();
        }
        protected void Movimentacao(string op, double valor)
        {
            string[] arquivo = CriarArquivoTxt();
            string linha =  DateTime.Now + " " + op + Convert.ToString(valor);
            string[] novo_arquivo = new string[arquivo.Length+1];
            
            for (int i = 0; i < arquivo.Length; i++)
            {
                novo_arquivo[i] = arquivo[i];
            }

            novo_arquivo[arquivo.Length] = linha;
            File.WriteAllLines(numero+".txt",novo_arquivo);
        }
        
    }
}