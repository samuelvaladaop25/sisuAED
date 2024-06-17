public class Candidato
{
    public string Nome { get; private set; }
    public float NotaMedia { get; private set; }
    public int NotaRedacao { get; private set; }
    public int NotaMat { get; private set; }
    public int NotaLing { get; private set; }
    public int Opcao1 { get; private set; }
    public int Opcao2 { get; private set; }
    public bool aprovadoOpcao1 { get; set; }
    public bool aprovadoOpcao2 { get; set; }

    public bool esperaOpcao1 { get; set; }
    public bool esperaOpcao2 { get; set; }
    public Candidato(string nome, int notaRedacao, int notaMat, int notaLing, int opcao1, int opcao2)
    {
        Nome = nome;
        NotaRedacao = notaRedacao;
        NotaMat = notaMat;
        NotaLing = notaLing;
        Opcao1 = opcao1;
        aprovadoOpcao1 = false;
        aprovadoOpcao2 = false;
        esperaOpcao1 = false;
        esperaOpcao2 = false;
        Opcao2 = opcao2;
        NotaMedia = (Convert.ToSingle(notaRedacao) + Convert.ToSingle(notaMat) + Convert.ToSingle(notaLing)) / 3;
    }

    public override string ToString()
    {
        return $"{Nome};{NotaRedacao};{NotaMat};{NotaLing};{Opcao1};{Opcao2}";
    }
}

