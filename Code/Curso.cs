public class Curso {

    public int CodigoId { get; private set; }
    public string Nome { get; private set; }
    public int NumVagas { get; private set; }
    public List<Candidato> Aprovados { get; set; } = new List<Candidato>();
    public Fila Espera { get; set; } = new Fila(10);

    public Curso(int codigoId, string nome, int numVagas)
    {
        CodigoId = codigoId;
        Nome = nome;
        NumVagas = numVagas;
    }

    public override string ToString()
    {
        return $"Curso: {Nome} - Vagas: {NumVagas}";
    }
    public float PegarNotaCorte()
    {
        if (Aprovados.Count == 0)
            return 0;
        return Aprovados.Min(candidato => candidato.NotaMedia);
    }
}
