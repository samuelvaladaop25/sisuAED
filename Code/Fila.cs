public class Fila
{
    Aluno[] array;
    int primeiro, ultimo;
    public Fila(int tamanho)
    {
        array = new Aluno[tamanho + 1];
        primeiro = ultimo = 0;
    }
    void Inserir(Aluno x)
    {
        if (((ultimo + 1) % array.Length) != primeiro)
        {
            array[ultimo] = x;
            ultimo = (ultimo + 1) % array.Length;
        }
    }
    Aluno Remover()
    {
        if (primeiro == ultimo)
            throw new Exception("Erro! Fila Vazia");
        Aluno resp = array[primeiro];
        primeiro = (primeiro + 1) % array.Length;
        return resp;
    }
    void Mostrar()
    {
        int i = primeiro;
        Console.Write("[");
        while (i != ultimo)
        {
            Console.Write(array[i].Nome + " ");
            i = (i + 1) % array.Length;
        }
        Console.WriteLine("]");
    }

    void OrdenaFila(){
        
    }
}