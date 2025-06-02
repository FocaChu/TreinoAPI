namespace TreinoAPI.Models
{
    public enum Genero
    {
        Acao,
        Aventura,
        Comedia,
        Drama,
        Fantasia,
        FiccaoCientifica,
        Terror
    }

    public class FilmeModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public Genero Genero { get; set; }
    }
}
