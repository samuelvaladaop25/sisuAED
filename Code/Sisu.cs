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


            Dictionary<int, Curso> cursos = getCursos(linhas, numCursos);
            Candidato[] candidatos = getCandidatos(linhas, numCandidatos, numCursos + 1);





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

        public static Dictionary<int, Curso> getCursos(string[] linhas, int numCursos)
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

        public static Candidato[] getCandidatos(string[] linhas, int numCandidatos, int linhaInicial)
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


    }
}
