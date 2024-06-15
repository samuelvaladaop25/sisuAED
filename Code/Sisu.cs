using System;
using System.Text;

namespace sisuAED
{
    class Sisu
    {
        static void Main(string[] args)
        {
            string[] linhas;

            linhas = LeArquivo("../../../Artefatos/entradaOrdenadaTeste.txt");

            int numCursos, numCandidatos;
            numCursos = int.Parse(linhas[0].Split(';')[0]);
            numCandidatos = int.Parse(linhas[0].Split(';')[1]);


            Dictionary<int, Curso> cursos = GetCursos(linhas, numCursos);
            Candidato[] candidatos = GetCandidatos(linhas, numCandidatos, numCursos + 1);

            Selecao(cursos, candidatos);





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
                    throw new Exception($"Linha {i} inv�lida!");
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
                    throw new Exception($"Linha {i} inv�lida!");
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
                            candidatos[i].aprovadoOpcao1 = true;
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

                    for (int i = 0; cursos[codigoCurso].Espera.Contar() < 10; i++)
                    {
                        try
                        {
                            if (candidatos[i].Opcao1 == codigoCurso && candidatos[i].aprovadoOpcao1 == false)
                            {
                                cursos[codigoCurso].Espera.Inserir(candidatos[i]);
                                candidatos[i].esperaOpcao1 = true;
                            }
                        }
                        catch (Exception)
                        {
                            // Caso nao entre, Significa que nao há mas candidatos para verificar mesmo com vagas
                            break;
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

                    for (int i = 0; cursos[codigoCurso].Espera.Contar() < 10; i++)
                    {
                        try
                        {
                            if (candidatos.Length > i)
                            {  // Caso nao entre, Significa que nao há mas candidatos para verificar mesmo com vagas
                                if (candidatos[i].Opcao2 == codigoCurso && candidatos[i].aprovadoOpcao2 == false)
                                {
                                    cursos[codigoCurso].Espera.Inserir(candidatos[i]);
                                    candidatos[i].esperaOpcao2 = true;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            // Caso nao entre, Significa que nao há mas candidatos para verificar mesmo com vagas
                            break;
                        }
                    }

                }
            }
        }
    }
}
