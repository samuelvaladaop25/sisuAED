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
                    throw new Exception($"Linha {i} inválida!");
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
                    throw new Exception($"Linha {i} inválida!");
            }

            return candidatos;
        }

        public void Selecao(Dictionary<int, Curso> cursos, Candidato[] candidatos)
        {
            // ordenação


            foreach (int codigoCurso in cursos.Keys)
            {
                // 31984762337
                // pegar os numVagas na quantidade dos alunos que tem o curso como primeira opção

                // numVagas nao foi preenchido?

                // se tiver sobrando, colocar o curso na fila de espera
            }

        }
    }
}
