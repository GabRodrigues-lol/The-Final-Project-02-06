namespace TheFinalProject
{
    public class Emprestimo
    {
        public Livro Livro { get; set; }
        public Usuario Usuario { get; set; }
        public PeriodoEmprestimo Periodo { get; set; }
        public bool Finalizado => Periodo.DataDevolucao.HasValue;
    }
}