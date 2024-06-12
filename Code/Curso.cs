public class Curso {
    private int codigoId;
    private string nome;
    private int numVagas;
    public List<Aluno> aprovados;
    public Fila espera = new Fila(10);

    public Curso(int codigoId, string nome, int numVagas){
        this.codigoId=codigoId;
        this.nome=nome;
        this.numVagas=numVagas;
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
    

}

