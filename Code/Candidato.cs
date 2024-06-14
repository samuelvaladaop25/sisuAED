public class Candidato
{
    public string Nome { get; private set; }
    public int NotaMedia { get; private set; }
    public int NotaRedacao { get; private set; }
    public int NotaMat { get; private set; }
    public int NotaLing { get; private set; }
    public int Opcao1 { get; private set; }
    public int Opcao2 { get; private set; }

    // Constructor
    public Candidato(string nome, int notaRedacao, int notaMat, int notaLing, int opcao1, int opcao2)
    {
        Nome = nome;
        NotaRedacao = notaRedacao;
        NotaMat = notaMat;
        NotaLing = notaLing;
        Opcao1 = opcao1;
        Opcao2 = opcao2;
        NotaMedia = (notaRedacao + notaMat + notaLing) / 3;
    }

    public override string ToString()
    {
        return $"{Nome};{NotaRedacao};{NotaMat};{NotaLing};{Opcao1};{Opcao2}";
    }
}

