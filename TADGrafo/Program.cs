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
            
            GraphNode<string> v1 = new GraphNode<string>("Index.htm");
            GraphNode<string> v2 = new GraphNode<string>("Home.htm");
            GraphNode<string> v3 = new GraphNode<string>("Contact.htm");
            GraphNode<string> v4 = new GraphNode<string>("About.htm");
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

            Console.WriteLine(Aresta<string>.GetAresta(listaArestas.FirstOrDefault(a=>a.from==v2)));

            Console.Read();
        }
    }
}
