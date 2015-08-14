using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TADGrafo
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph<string> grafo = new Graph<string>();
            
            Vertice<string> v1 = new Vertice<string>("Index.htm");
            Vertice<string> v2 = new Vertice<string>("Home.htm");
            Vertice<string> v3 = new Vertice<string>("Contact.htm");
            Vertice<string> v4 = new Vertice<string>("About.htm");
            //InserirVertice()
            grafo.InserirVertice(v1);
            grafo.InserirVertice(v2);
            grafo.InserirVertice(v3);
            grafo.InserirVertice(v4);


            var lista = grafo.Vertices().ToList();
            foreach (Node<string> vertice in lista)
            {
                Console.WriteLine(vertice.Value.ToString());
            }
            Console.WriteLine();

            //InserirVerticeDirecionado()
            Aresta<string> aresta1 = grafo.InserirArestaDirecionada(v1, v2, 1);
            Aresta<string> aresta2 = grafo.InserirArestaDirecionada(v1, v3, 2);
            Aresta<string> aresta3 = grafo.InserirArestaDirecionada(v1, v4, 3);
            Aresta<string> aresta4 = grafo.InserirAresta(v2, v3, 3);

            List<Aresta<string>> listaArestas = grafo.Arestas();
            foreach (Aresta<string> arestaItem in listaArestas)
            {
                Console.WriteLine(Aresta<string>.GetAresta(arestaItem));
            }
         
            //eAdjacente()
            if (grafo.eAdjacente(v1, v2)) Console.WriteLine(v1.Value.ToString() + " é adjacente de " + v2.Value.ToString());
            else Console.WriteLine(v1.Value.ToString() + " não é adjacente de " + v2.Value.ToString());

            //removerVertice
            //bool verticeRemovido = grafo.RemoverVertice(v2);

            Console.WriteLine(grafo.oposto(v1, aresta1).Value.ToString());
            Console.Read();
        }
    }
}
