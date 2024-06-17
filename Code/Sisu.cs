using System;
using System.Text;

namespace sisuAED
{
    class Sisu
    {
        static void Main(string[] args)
{
    string[] linhas;

    linhas = LeArquivo("../../../Artefatos/entrada.txt");

    int numCursos, numCandidatos;
    numCursos = int.Parse(linhas[0].Split(';')[0]);
    numCandidatos = int.Parse(linhas[0].Split(';')[1]);

    Dictionary<int, Curso> cursos = GetCursos(linhas, numCursos);
    Candidato[] candidatos = GetCandidatos(linhas, numCandidatos, numCursos + 1);

    Selecao(cursos, candidatos);

    EscreverSaida(cursos, "../../../Artefatos/saida.txt");
}


        public static string[] LeArquivo(string caminho)
        {
            LinkedList<string> list = new LinkedList<string>();

            try
            {
                using StreamReader reader = new(caminho, Encoding.GetEncoding("ISO-8859-1"));

                string line;

                do
                {
                    line = reader.ReadLine();

                    if (line == null)
                        break;
                    list.AddLast(line);

                } while (true);
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return list.ToArray();
        }

        public static Dictionary<int, Curso> GetCursos(string[] linhas, int numCursos)
        {
            Dictionary<int, Curso> cursos = new Dictionary<int, Curso>();

            for (int i = 0; i < numCursos; i++)
            {
                string[] linhaDividida = linhas[i + 1].Split(';');

                if (!String.IsNullOrEmpty(linhaDividida[0]) && linhaDividida.Length == 3)
                {
                    Curso curso = new Curso(int.Parse(linhaDividida[0]), linhaDividida[1], int.Parse(linhaDividida[2]));
                    cursos.Add(curso.CodigoId, curso);
                }

                else
                    throw new Exception($"Linha {i} invalida!");
            }

            return cursos;
        }

        public static Candidato[] GetCandidatos(string[] linhas, int numCandidatos, int linhaInicial)
        {
            Candidato[] candidatos = new Candidato[numCandidatos];

            for (int i = 0; i < numCandidatos; i++)
            {
                string[] linhaDividida = linhas[i + linhaInicial].Split(';');

                if (!String.IsNullOrEmpty(linhaDividida[0]) && linhaDividida.Length == 6)
                    candidatos[i] = new Candidato(linhaDividida[0], int.Parse(linhaDividida[1]), int.Parse(linhaDividida[2]), int.Parse(linhaDividida[3]), int.Parse(linhaDividida[4]), int.Parse(linhaDividida[5]));

                else
                    throw new Exception($"Linha {i} invalida!");
            }

            return candidatos;
        }

        public static void Selecao(Dictionary<int, Curso> cursos, Candidato[] candidatos)
        {
            // 31984762337

            // AQUI O ARRAY CANDIDATOS PRECISA ESTAR ORDENADO DE FORMA DECRESCENTE DA NOTA MÉDIA

            // 1- Inserir na Lista de aprovados do curso y os x (numVagas) os candidatos com a opção 1 de curso y e dar true no aprovadoop1
            // 2- Inserir lista de espera da do curso y aqueles que tem op1 = y
            // 3- Inserir na Lista de aprovados do curso y os x (numVagas) os candidatos com a opção 2 de curso y que nao foram aprovados na op1 e dar true no aprovadoop2
            // 4- Inserir lista de espera da do curso y aqueles que tem op2 = y

            candidatos = QuickSort(candidatos, 0, candidatos.Length-1);


            foreach (int codigoCurso in cursos.Keys)
            {
                // For enquanto lista de aprovados for menor que o numero de vagas
                for (int i = 0; cursos[codigoCurso].Aprovados.Count < cursos[codigoCurso].NumVagas; i++)
                {
                    try
                    {
                        if (candidatos[i].Opcao1 == codigoCurso)
                        {
                            cursos[codigoCurso].Aprovados.Add(candidatos[i]);

                            // samuel esqueceu: remove ele da lista de espera e de aprovados da segunda opcao
                            PassouOpcao1(cursos, candidatos[i]);
                        }
                    }
                    catch (Exception)
                    {
                        // Caso nao entre, Significa que nao há mas candidatos para verificar mesmo com vagas
                        break;
                    }
                }

                if (cursos[codigoCurso].Aprovados.Count == cursos[codigoCurso].NumVagas)
                {
                    //Curso está sem vagas, usar fila de espera
                    int tamEspera = cursos[codigoCurso].Espera.GetQuantidade();// samuel tinha botado dentro da condição do for e isso gasta processamento atoa
                    for (int i = 0; tamEspera < 10 && i < candidatos.Length; i++)// samuel nao fez parar ao atingir o número de candidatos, isso aqui tava fazendo com que a função demorava 25 segundos pra rodar, nao tinha condição de parada...
                    {
                        // o candidato deve ter o curso como opção1 e não estar já aprovado
                        if (candidatos[i].Opcao1 == codigoCurso && candidatos[i].aprovadoOpcao1 == false)
                        {
                            cursos[codigoCurso].Espera.Inserir(candidatos[i]);
                            candidatos[i].esperaOpcao1 = true;
                            tamEspera++; // por causa do contar do For
                        }
                    }
                }

                for (int i = 0; cursos[codigoCurso].Aprovados.Count < cursos[codigoCurso].NumVagas; i++)
                {
                    try
                    {
                        if (candidatos[i].Opcao2 == codigoCurso && candidatos[i].aprovadoOpcao1 == false)
                        {
                            cursos[codigoCurso].Aprovados.Add(candidatos[i]);
                            candidatos[i].aprovadoOpcao2 = true;
                        }
                    }
                    catch (Exception)
                    {
                        // Caso nao entre, Significa que nao há mas candidatos para verificar mesmo com vagas
                        break;
                    }
                }

                if (cursos[codigoCurso].Aprovados.Count == cursos[codigoCurso].NumVagas)
                {
                    //Curso está sem vagas, usar fila de espera
                    int tamEspera = cursos[codigoCurso].Espera.GetQuantidade(); // samuel tinha botado dentro da condição do for e isso gasta processamento atoa
                    for (int i = 0; tamEspera < 10 && i < candidatos.Length; i++) // samuel nao fez parar ao atingir o número de candidatos, isso aqui tava fazendo com que a função demorava 25 segundos pra rodar, nao tinha condição de parada...
                    {
                        if (candidatos[i].Opcao2 == codigoCurso && candidatos[i].aprovadoOpcao2 == false && candidatos[i].aprovadoOpcao1 == false) // ele nao pode entrar na fila de espera da opcao2 estando aprvado na opcao1
                        {
                            cursos[codigoCurso].Espera.Inserir(candidatos[i]);
                            candidatos[i].esperaOpcao2 = true;
                            tamEspera++; // por causa do contar do For
                        }
                    }

                }
            }
        }
        // faz a fila de espera da opcao2 andar
        public static void PassouOpcao1(Dictionary<int, Curso> cursos, Candidato candidato)
        {
            Curso cursoOpcao2 = cursos[candidato.Opcao2];
            Candidato? primeiroEsperaOpcao2 = cursoOpcao2.Espera.GetPrimeiro();

            if (candidato.aprovadoOpcao2)
            {
                cursoOpcao2.Aprovados.Remove(candidato);

                Candidato? primeiroEspera = cursoOpcao2.Espera.GetPrimeiro();

                if (primeiroEspera != null)
                {
                    cursoOpcao2.Espera.Remover();
                    cursoOpcao2.Aprovados.Add(primeiroEspera);

                    // verifica em qual das opcoes ela foi adicionada
                    if (primeiroEspera.Opcao1 == cursoOpcao2.CodigoId)
                    {
                        primeiroEspera.aprovadoOpcao1 = true;
                        primeiroEspera.esperaOpcao1 = false;
                        PassouOpcao1(cursos, primeiroEspera);
                    }
                    else
                    {
                        primeiroEspera.aprovadoOpcao2 = true;
                        primeiroEspera.esperaOpcao2 = false;
                    }

                }
            }

            else if (primeiroEsperaOpcao2 != null && primeiroEsperaOpcao2 == candidato)
                cursoOpcao2.Espera.Remover();

            candidato.aprovadoOpcao1 = true;
            candidato.esperaOpcao1 = false;
            candidato.aprovadoOpcao2 = false;
            candidato.esperaOpcao2 = false;
        }

        static Candidato[] QuickSort(Candidato[] array, int esq, int dir)
        {
            int l = esq, d = dir, pivo = array[(esq + dir) / 2].NotaMedia;
            Candidato aux;

            while (l <= d)
            {
                while (array[l].NotaMedia > pivo) l++;
                while (array[d].NotaMedia < pivo) d--;

                if (l <= d)
                {
                    aux = array[l];
                    array[l] = array[d];
                    array[d] = aux;

                    l++;
                    d--;
                }
            }

            if (esq < d)
                QuickSort(array, esq, d);
            if (l < dir)
                QuickSort(array, l, dir);

            return array;
        }
public static void EscreverSaida(Dictionary<int, Curso> cursos, string caminho)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(caminho, false, Encoding.GetEncoding("ISO-8859-1")))
                {
                    foreach (var curso in cursos.Values)
                    {
                
                        writer.WriteLine($"\n{curso.Nome}; {curso.PegarNotaCorte():F2}");
                        writer.WriteLine("Selecionados");
                        foreach (var candidato in curso.Aprovados)
                        {
                            writer.WriteLine($"{candidato.Nome} {candidato.NotaMedia:F2} {candidato.NotaRedacao} {candidato.NotaMat} {candidato.NotaLing}");
                        }
                        writer.WriteLine("Fila de Espera");
                        int i = curso.Espera.primeiro;
                        int count = curso.Espera.GetQuantidade();
                        while (count > 0)
                        {
                            var candidato = curso.Espera.array[i];
                            writer.WriteLine($"{candidato.Nome} {candidato.NotaMedia:F2} {candidato.NotaRedacao} {candidato.NotaMat} {candidato.NotaLing}");
                            i = (i + 1) % curso.Espera.array.Length;
                            count--;
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("O arquivo não pôde ser escrito:");
                Console.WriteLine(e.Message);
            }
        }
    }
}