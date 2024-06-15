public class Fila
{
    Candidato[] array;
    int primeiro, ultimo;
    public Fila(int tamanho)
    {
        array = new Candidato[tamanho + 1];
        primeiro = ultimo = 0;
    }
    public void Inserir(Candidato x)
    {
        if (((ultimo + 1) % array.Length) != primeiro)
        {
            array[ultimo] = x;
            ultimo = (ultimo + 1) % array.Length;
        }
    }
    public Candidato Remover()
    {
        if (primeiro == ultimo)
            throw new Exception("Erro! Fila Vazia");
        Candidato resp = array[primeiro];
        primeiro = (primeiro + 1) % array.Length;
        return resp;
    }
    public void Mostrar()
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

    public int Contar()
    {
        if (primeiro == ultimo)
            return 0;
        int i = primeiro, contador = 0; ;
        while (i != ultimo)
        {
            contador++;
            i = (i + 1) % array.Length;
        }
        return contador;

    }
}