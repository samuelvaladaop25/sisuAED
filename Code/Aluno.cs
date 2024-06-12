public class Aluno {
    private int codigoId;
    private string nome;
    private int notaMedia, notaRedacao, notaMat, notaLing;
    private int opcao1, opcao2;

    public Aluno(int codigoId, string nome, int notaRedacao, int notaMat, int notaLing, int opcao1, int opcao2){
        this.codigoId=codigoId;
        this.nome=nome;
        notaMedia= (notaRedacao + notaMat + notaLing) /3;
        this.notaRedacao=notaRedacao;
        this.notaMat=notaMat;
        this.notaLing=notaLing;
        this.opcao1 = opcao1;
        this.opcao2=opcao2;
    }

    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }

    public int CodigoId
    {
        get { return codigoId; }
        set { codigoId = value; }
    }

    public int NotaMedia
    {
        get { return notaMedia; }
    }

    public int NotaRedacao
    {
        get { return notaRedacao; }
        set { notaRedacao = value; }
    }

    public int NotaMat
    {
        get { return notaMat; }
        set { notaMat = value; }
    }

    public int NotaLing
    {
        get { return notaLing; }
        set { notaLing = value; }
    }

    public int Opcao1{
        get { return opcao1; }
        set { opcao1 = value;}
    }

    public int Opcao2{
        get { return opcao2; }
        set { opcao2 = value;}
    }

    public void CalcularMedia(){
        notaMedia = (notaRedacao + notaMat + notaLing) /3;
    }
}

