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

            QuickSort(candidatos, 0, candidatos.Length - 1, Comparar);


            foreach (int codigoCurso in cursos.Keys)
            {
                for (int i = 0; cursos[codigoCurso].Aprovados.Count < cursos[codigoCurso].NumVagas; i++)
                {
                    try
                    {
                        if (candidatos[i].Opcao1 == codigoCurso)
                        {
                            cursos[codigoCurso].Aprovados.Add(candidatos[i]);

                            PassouOpcao1(cursos, candidatos[i]);
                        }
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

                if (cursos[codigoCurso].Aprovados.Count == cursos[codigoCurso].NumVagas)
                {
                    int tamEspera = cursos[codigoCurso].Espera.GetQuantidade();
                    for (int i = 0; tamEspera < 10 && i < candidatos.Length; i++)
                    {
                        if (candidatos[i].Opcao1 == codigoCurso && candidatos[i].aprovadoOpcao1 == false)
                        {
                            cursos[codigoCurso].Espera.Inserir(candidatos[i]);
                            candidatos[i].esperaOpcao1 = true;
                            tamEspera++;
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
                        break;
                    }
                }

                if (cursos[codigoCurso].Aprovados.Count == cursos[codigoCurso].NumVagas)
                {
                    int tamEspera = cursos[codigoCurso].Espera.GetQuantidade();
                    for (int i = 0; tamEspera < 10 && i < candidatos.Length; i++)
                    {
                        if (candidatos[i].Opcao2 == codigoCurso && candidatos[i].aprovadoOpcao2 == false && candidatos[i].aprovadoOpcao1 == false)
                        {
                            cursos[codigoCurso].Espera.Inserir(candidatos[i]);
                            candidatos[i].esperaOpcao2 = true;
                            tamEspera++;
                        }
                    }

                }
            }
        }
    
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
            else if (candidato.esperaOpcao1)
                cursos[candidato.Opcao1].Espera = CopiarFilaSemElemento(cursos[candidato.Opcao1].Espera, candidato);
            else if (candidato.esperaOpcao2)
                cursos[candidato.Opcao2].Espera = CopiarFilaSemElemento(cursos[candidato.Opcao2].Espera, candidato);


            candidato.aprovadoOpcao1 = true;
            candidato.esperaOpcao1 = false;
            candidato.aprovadoOpcao2 = false;
            candidato.esperaOpcao2 = false;
        }

        public static int Comparar(Candidato x, Candidato y)
        {
            int resultado = y.NotaMedia.CompareTo(x.NotaMedia);
            if (resultado == 0)
            {
                resultado = y.NotaRedacao.CompareTo(x.NotaRedacao);
                if (resultado == 0)
                {
                    resultado = y.NotaMat.CompareTo(x.NotaMat);
                    if (resultado == 0)
                    {
                        resultado = y.NotaLing.CompareTo(x.NotaLing);
                    }
                }
            }
            return resultado;
        }

        public static void QuickSort(Candidato[] array, int esq, int dir, Func<Candidato, Candidato, int> comparar)
        {
            int l = esq, d = dir;
            Candidato pivo = array[(esq + dir) / 2];
            Candidato aux;

            while (l <= d)
            {
                while (comparar(array[l], pivo) < 0) l++;
                while (comparar(array[d], pivo) > 0) d--;

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
                QuickSort(array, esq, d, comparar);
            if (l < dir)
                QuickSort(array, l, dir, comparar);
        }

        public static Fila CopiarFilaSemElemento(Fila original, Candidato elementoParaRemover)
        {
            Fila copia = new Fila(original.array.Length);
            for (int i = 0; i < original.quantidade; i++)
            {
                int index = (original.primeiro + i) % original.array.Length;
                if (!original.array[index].Equals(elementoParaRemover))
                {
                    copia.Inserir(original.array[index]);
                }
            }
            return copia;
        }

        public static void EscreverSaida(Dictionary<int, Curso> cursos, string caminho)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(caminho, false, Encoding.GetEncoding("ISO-8859-1")))
                {
                    foreach (Curso curso in cursos.Values)
                    {
                        writer.WriteLine($"{curso.Nome} {curso.PegarNotaCorte():F2}");
                        writer.WriteLine("Selecionados");
                        foreach (Candidato candidato in curso.Aprovados)
                        {
                            writer.WriteLine($"{candidato.Nome} {candidato.NotaMedia:F2} {candidato.NotaRedacao} {candidato.NotaMat} {candidato.NotaLing}");
                        }
                        writer.WriteLine("Fila de Espera");
                        int i = curso.Espera.primeiro;
                        for (int count = curso.Espera.GetQuantidade(); count > 0; count--)
                        {
                            Candidato candidato = curso.Espera.array[i];
                            writer.WriteLine($"{candidato.Nome} {candidato.NotaMedia:F2} {candidato.NotaRedacao} {candidato.NotaMat} {candidato.NotaLing}");
                            i = (i + 1) % curso.Espera.array.Length;
                        }
                        writer.WriteLine();
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be written:");
                Console.WriteLine(e.Message);
            }
        }
    }
}