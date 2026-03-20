namespace ConsultoriaDev.Models
{
    public class Solicitacao
    {
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public string DescricaoProblema { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string Status { get; set; }
    }
}
