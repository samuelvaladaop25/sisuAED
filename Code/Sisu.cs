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

            // pega o número de cursos e candidatos
            int numCursos, numCandidatos;
            numCursos = int.Parse(linhas[0].Split(';')[0]);
            numCandidatos = int.Parse(linhas[0].Split(';')[1]);

            // Cria os objetos
            Curso[] cursos = getCursos(linhas, numCursos, 1);
            Candidato[] candidatos = getCandidatos(linhas, numCandidatos, numCursos + 1);





        }

        public static string[] LeArquivo(string caminho)
        {
            LinkedList<string> list = new LinkedList<string>();

            try
            {
                // Open the text file using a stream reader.
                using StreamReader reader = new(caminho, Encoding.GetEncoding("ISO-8859-1"));

                string line;

                do
                {
                    // Read the stream as a string.
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

        public static Curso[] getCursos(string[] linhas, int numCursos, int linhaInicial)
        {
            Curso[] cursos = new Curso[numCursos];

            for (int i = 0; i < numCursos; i++)
            {
                string[] linhaDividida = linhas[i + linhaInicial].Split(';');

                // Cria os cursos com as partes da linha
                if (!String.IsNullOrEmpty(linhaDividida[0]) && linhaDividida.Length == 3)
                    cursos[i] = new Curso(int.Parse(linhaDividida[0]), linhaDividida[1], int.Parse(linhaDividida[2]));

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

                // Cria os Candidatos com as partes da linha
                if (!String.IsNullOrEmpty(linhaDividida[0]) && linhaDividida.Length == 6)
                    candidatos[i] = new Candidato(linhaDividida[0], int.Parse(linhaDividida[1]), int.Parse(linhaDividida[2]), int.Parse(linhaDividida[3]), int.Parse(linhaDividida[4]), int.Parse(linhaDividida[5]));

                else
                    throw new Exception($"Linha {i} inválida!");
            }

            return candidatos;
        }
    }
}
