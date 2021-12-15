using System;
using System.IO;

namespace Santo_Andre
{
    class Program
    {
        static void Main(string[] args)
        {
            ContaPF[] ContasPF = CriarArrayContasPF("ContasPF.txt");
            ContaPJ[] ContasPJ = CriarArrayContasPJ("ContasPJ.txt");
            Notas notas = new Notas();
            notas.Valor_caixa = notas.defineValorCaixa();
            //Console.WriteLine("debug: notas.Valor_caixa: "+notas.Valor_caixa);

            Console.Clear();
            string conta = string.Empty;
            while (conta != "sair")
            {
                
                Console.WriteLine("Digite numero da conta:");
                conta = Console.ReadLine();
                if (validarcontaPF(conta,ContasPF))
                {
                    int contaAtual = DefinirConta(conta,ContasPF);
                    if (ContasPF[contaAtual].UsuarioBloqueado)
                    {
                        Console.WriteLine("Acesso Negado.");
                        continue;
                    }
                    else if (ContasPF[contaAtual].verificarSenha())
                    {
                        Console.WriteLine("Olá "+ContasPF[contaAtual].Nome);

                        string escolha = string.Empty;
                        while (escolha != "0")
                        {
                            Console.Clear();
                            Console.WriteLine("Digite sua opção:");
                            Console.WriteLine("1-Extrato  |   2-Depósito    |   3-Saque |   4-Alterar Senha |   0-Sair");
                            escolha = Console.ReadLine();
                            if (escolha == "0")
                            {
                                continue;
                            }
                            else if (escolha == "1")
                            {
                                Console.Clear();
                                ContasPF[contaAtual].Extrato();
                            }
                            else if (escolha == "2")
                            {
                                Console.Clear();
                                ContasPF[contaAtual].Deposito();
                            }
                            else if (escolha == "3")
                            {
                                Console.Clear();
                                ContasPF[contaAtual].SaquePF(notas);
                            }
                            else if (escolha == "4")
                            {
                                Console.Clear();
                                ContasPF[contaAtual].AlterarSenha();
                            }
                            else
                            {
                                Console.WriteLine("Opção Inválida.");
                            }
                        }

                    }
                    else
                    {
                        // Console.WriteLine("debug: erro ao verificarSenha()");
                    }
                    atualizarTxtContaPF(ContasPF,"ContasPF.txt");
                    atualizarTxtContaPJ(ContasPJ,"ContasPJ.txt");
                }
                else if (validarcontaPJ(conta,ContasPJ))
                {
                     int contaAtual = DefinirConta(conta,ContasPJ);
                    if (ContasPJ[contaAtual].UsuarioBloqueado)
                    {
                        Console.WriteLine("Acesso Negado.");
                        break;
                    }
                    else if (ContasPJ[contaAtual].verificarSenha())
                    {
                        Console.WriteLine("Olá "+ContasPJ[contaAtual].Nome);

                        string escolha = string.Empty;
                        while (escolha != "0")
                        {
                            Console.Clear();
                            Console.WriteLine("Digite sua opção:");
                            Console.WriteLine("1-Extrato  |   2-Depósito    |   3-Saque |   4-Alterar Senha |   5-Pagamentos    |   0-Sair");
                            escolha = Console.ReadLine();

                            if (escolha == "1")
                            {
                                Console.Clear();
                                ContasPJ[contaAtual].Extrato();
                            }
                            else if (escolha == "2")
                            {
                                Console.Clear();
                                ContasPJ[contaAtual].Deposito();
                            }
                            else if (escolha == "3")
                            {
                                Console.Clear();
                                ContasPJ[contaAtual].SaquePJ(notas);
                            }
                            else if (escolha == "4")
                            {
                                Console.Clear();
                                ContasPJ[contaAtual].AlterarSenha();
                            }
                            else if (escolha == "5")
                            {
                                Console.Clear();
                                ContasPJ[contaAtual].Pagamento(ContasPF);
                            }
                            else
                            {
                                Console.WriteLine("Opção Inválida.");
                                Console.WriteLine("\n\n Pressione Enter para voltar");
                                Console.ReadLine();
                            }
                        }

                    }
                    else
                    {
                        // Console.WriteLine("debug: erro ao verificarSenha()");
                    }
                    atualizarTxtContaPF(ContasPF,"ContasPF.txt");
                    atualizarTxtContaPJ(ContasPJ,"ContasPJ.txt");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Conta Inválida.");
                }
            }
        }
        static bool validarcontaPF(string numero_conta, ContaPF[] ArrayContas)
        {
            for (int i = 0; i < ArrayContas.Length; i++)
            {
                if (ArrayContas[i].Numero == numero_conta)
                {
                    return true;
                }
            }
            return false;
        }
        static bool validarcontaPJ(string numero_conta, ContaPJ[] ArrayContas)
        {
            for (int i = 0; i < ArrayContas.Length; i++)
            {
                if (ArrayContas[i].Numero == numero_conta)
                {
                    return true;
                }
            }
            return false;
        }
        static ContaPF[] CriarArrayContasPF(string path)
        {
            string[] arquivo = File.ReadAllLines(path);
            ContaPF user = new ContaPF();
            ContaPF[] ArrayContas = new ContaPF[((arquivo.Length+1)/6)] ;
            int cont = 0;

            for (int j = 0; j < ArrayContas.Length; j++)
            {
                ArrayContas[j] = new ContaPF();
            }
            // Console.WriteLine("debug: "+arquivo.Length);
            // Console.WriteLine("debug: "+ArrayContas.Length);

            for (int i = 0; i < arquivo.Length; i+=6)
            {
                ArrayContas[cont].Numero = arquivo[i];
                ArrayContas[cont].Nome = arquivo[i+1];
                ArrayContas[cont].Saldo = double.Parse(arquivo[i+2]);
                ArrayContas[cont].Senha = int.Parse(arquivo[i+3]);
                ArrayContas[cont].UsuarioBloqueado = bool.Parse(arquivo[i+4]);

                // Console.WriteLine("debug: "+ArrayContas[cont].Numero);
                // Console.WriteLine("debug: "+ArrayContas[cont].Nome);
                // Console.WriteLine("debug: "+ArrayContas[cont].Saldo);
                // Console.WriteLine("debug: "+ArrayContas[cont].Senha);

                cont++;
            }
            return ArrayContas;
        }
        static ContaPJ[] CriarArrayContasPJ(string path)
        {
            string[] arquivo = File.ReadAllLines(path);
            ContaPJ user = new ContaPJ();
            ContaPJ[] ArrayContas = new ContaPJ[((arquivo.Length+1)/6)] ;
            int cont = 0;

            for (int j = 0; j < ArrayContas.Length; j++)
            {
                ArrayContas[j] = new ContaPJ();
            }
            // Console.WriteLine("debug: "+arquivo.Length);
            // Console.WriteLine("debug: "+ArrayContas.Length);

            for (int i = 0; i < arquivo.Length; i+=6)
            {
                ArrayContas[cont].Numero = arquivo[i];
                ArrayContas[cont].Nome = arquivo[i+1];
                ArrayContas[cont].Saldo = double.Parse(arquivo[i+2]);
                ArrayContas[cont].Senha = int.Parse(arquivo[i+3]);
                ArrayContas[cont].UsuarioBloqueado = bool.Parse(arquivo[i+4]);

                // Console.WriteLine("debug: "+ArrayContas[cont].Numero);
                // Console.WriteLine("debug: "+ArrayContas[cont].Nome);
                // Console.WriteLine("debug: "+ArrayContas[cont].Saldo);
                // Console.WriteLine("debug: "+ArrayContas[cont].Senha);

                cont++;
            }
            return ArrayContas;
        }
        static void atualizarTxtContaPF(ContaPF[] ArrayContas,string path)
        {
            string[] arquivo = new string[(ArrayContas.Length*6)-1];
            int cont = 0;
            foreach (ContaPF conta in ArrayContas)
            {
                arquivo[cont] = conta.Numero;
                arquivo[cont+1] = conta.Nome;
                arquivo[cont+2] = Convert.ToString(conta.Saldo);
                arquivo[cont+3] = Convert.ToString(conta.Senha);
                arquivo[cont+4] = Convert.ToString(conta.UsuarioBloqueado);

                cont += 6;
            }
            File.WriteAllLines(path,arquivo);
        }
        static void atualizarTxtContaPJ(ContaPJ[] ArrayContas,string path)
        {
            string[] arquivo = new string[(ArrayContas.Length*6)-1];
            int cont = 0;
            foreach (ContaPJ conta in ArrayContas)
            {
                arquivo[cont] = conta.Numero;
                arquivo[cont+1] = conta.Nome;
                arquivo[cont+2] = Convert.ToString(conta.Saldo);
                arquivo[cont+3] = Convert.ToString(conta.Senha);
                arquivo[cont+4] = Convert.ToString(conta.UsuarioBloqueado);

                cont += 6;
            }
            File.WriteAllLines(path,arquivo);
        }
        static int DefinirConta(string conta, Conta[] ArrayContas)
        {
            int numero = 0;
            for (int i = 0; i < ArrayContas.Length; i++)
            {
                if (ArrayContas[i].Numero == conta)
                {
                    numero = i;
                }
            }
            return numero;
        }
    }
}