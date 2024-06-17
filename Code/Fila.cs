public class Fila
{
    public Candidato[] array;
    public int primeiro, ultimo, quantidade;
    public Fila(int tamanho)
    {
        array = new Candidato[tamanho];
        primeiro = ultimo = quantidade = 0;
    }
    public void Inserir(Candidato x)
    {
        if (quantidade < array.Length)
        {
            array[ultimo] = x;
            ultimo = (ultimo + 1) % array.Length;
            quantidade++;
        }
    }
    public Candidato Remover()
    {
        if (primeiro == ultimo)
            throw new Exception("Erro! Fila Vazia");
        Candidato resp = array[primeiro];
        primeiro = (primeiro + 1) % array.Length;
        quantidade--;

        return resp;
    }
    public int GetQuantidade()
    {
        return quantidade;
    }

    public Candidato GetPrimeiro()
    {
        if (primeiro != ultimo)
            return array[primeiro];

        return null;
    }
}