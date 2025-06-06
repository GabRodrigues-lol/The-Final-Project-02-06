using System;
using System.Collections.Generic;
namespace TheFinalProject
{

    public class Biblioteca
    {
        public List<Livro> Livros = new List<Livro>();
        public List<Usuario> Usuarios = new List<Usuario>();
        public List<Emprestimo> Emprestimos = new List<Emprestimo>();

        public Biblioteca()
        {
            Usuarios.Add(new Usuario { Nome = "Ana", Matricula = "U001" });
            Usuarios.Add(new Usuario { Nome = "Bruno", Matricula = "U002" });
            Usuarios.Add(new Usuario { Nome = "Carlos", Matricula = "U003" });

            Livros.Add(new Livro { Titulo = "1984", Autor = "George Orwell", ISBN = "ISBN001", Quantidade = 3 });
            Livros.Add(new Livro { Titulo = "Nada Pode Me Ferir", Autor = "David Goggins", ISBN = "ISBN002", Quantidade = 2 });
            Livros.Add(new Livro { Titulo = "A Revolução dos Bichos", Autor = "George Orwell", ISBN = "ISBN003", Quantidade = 1 });

            Emprestimos.Add(new Emprestimo
            {
                Usuario = Usuarios[0],
                Livro = Livros[0],
                Periodo = new PeriodoEmprestimo { DataEmprestimo = DateTime.Now, DataDevolucao = null }
            });
            Livros[0].Quantidade--;

            Emprestimos.Add(new Emprestimo
            {
                Usuario = Usuarios[1],
                Livro = Livros[1],
                Periodo = new PeriodoEmprestimo { DataEmprestimo = DateTime.Now, DataDevolucao = null }
            });
            Livros[1].Quantidade--;
        }

        public void CadastrarLivro()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine();
            Console.Write("Autor: ");
            string autor = Console.ReadLine();
            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();
            Console.Write("Quantidade: ");
            int quantidade = int.Parse(Console.ReadLine());

            Livros.Add(new Livro { Titulo = titulo, Autor = autor, ISBN = isbn, Quantidade = quantidade });
            Console.WriteLine("Livro cadastrado com sucesso!");
        }

        public void CadastrarUsuario()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Matrícula: ");
            string matricula = Console.ReadLine();

            Usuarios.Add(new Usuario { Nome = nome, Matricula = matricula });
            Console.WriteLine("Usuário cadastrado com sucesso!");
        }

        public void RegistrarEmprestimo()
        {
            Console.Write("Matrícula do usuário: ");
            string matricula = Console.ReadLine();
            Usuario usuario = Usuarios.Find(u => u.Matricula == matricula);
            if (usuario == null)
            {
                Console.WriteLine("Usuário não encontrado.");
                return;
            }

            Console.Write("ISBN do livro: ");
            string isbn = Console.ReadLine();
            Livro livro = Livros.Find(l => l.ISBN == isbn);
            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado.");
                return;
            }

            if (livro.Quantidade <= 0)
            {
                Console.WriteLine("Livro indisponível.");
                return;
            }

            livro.Quantidade -= 1;
            Emprestimos.Add(new Emprestimo
            {
                Usuario = usuario,
                Livro = livro,
                Periodo = new PeriodoEmprestimo
                {
                    DataEmprestimo = DateTime.Now,
                    DataDevolucao = null
                }
            });

            Console.WriteLine("Empréstimo registrado.");
        }

        public void RegistrarDevolucao()
        {
            Console.Write("Matrícula do usuário: ");
            string matricula = Console.ReadLine();
            Console.Write("ISBN do livro: ");
            string isbn = Console.ReadLine();

            Emprestimo emprestimo = Emprestimos.Find(e =>
                e.Usuario.Matricula == matricula &&
                e.Livro.ISBN == isbn &&
                !e.Finalizado);

            if (emprestimo == null)
            {
                Console.WriteLine("Empréstimo não encontrado ou já devolvido.");
                return;
            }

            var periodo = emprestimo.Periodo;
            periodo.DataDevolucao = DateTime.Now;
            emprestimo.Periodo = periodo;
            emprestimo.Livro.Quantidade += 1;

            Console.WriteLine("Devolução registrada.");
        }

        public void ListarLivros()
        {
            Console.WriteLine("\n--- Lista de Livros ---");
            foreach (var livro in Livros)
            {
                Console.WriteLine($"{livro.Titulo} - {livro.Autor} - ISBN: {livro.ISBN} - Disponível: {livro.Disponivel}");
            }
        }

        public void ListarUsuarios()
        {
            Console.WriteLine("\n--- Lista de Usuários ---");
            foreach (var usuario in Usuarios)
            {
                Console.WriteLine($"Nome: {usuario.Nome} | Matrícula: {usuario.Matricula}");
            }
        }


        public void ExibirRelatorios()
        {
            Console.WriteLine("\n--- Relatório ---");

            Console.WriteLine("\n Livros disponíveis:");
            foreach (var l in Livros)
                if (l.Disponivel > 0)
                    Console.WriteLine($"{l.Titulo} ({l.Disponivel})");

            Console.WriteLine("\n Livros emprestados:");
            foreach (var e in Emprestimos)
                if (!e.Finalizado)
                    Console.WriteLine($"{e.Livro.Titulo} emprestado para {e.Usuario.Nome}");

            Console.WriteLine("\n Usuários com livros:");
            foreach (var u in Usuarios)
            {
                int count = Emprestimos.FindAll(e => e.Usuario == u && !e.Finalizado).Count;
                if (count > 0)
                    Console.WriteLine($"{u.Nome} ({count} livro(s))");
            }
        }
    }
}