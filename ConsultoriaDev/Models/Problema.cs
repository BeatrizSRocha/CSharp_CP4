namespace ConsultoriaDev.Models
{
    public class Problema
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public int TempoResposta { get; set; }
    }
}
