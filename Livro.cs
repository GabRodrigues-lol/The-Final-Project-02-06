namespace TheFinalProject
{
    public class Livro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }

        private int quantidade;
        public int Quantidade
        {
            get { return quantidade; }
            set
            {
                if (value < 0)
                    throw new Exception("Quantidade não pode ser negativa.");
                quantidade = value;
            }
        }

        public int Disponivel => Quantidade;
    }

}
