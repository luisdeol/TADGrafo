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
            Grafo<string> grafo = new Grafo<string>();
            
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
            Aresta<string> aresta1 = grafo.InserirAresta(v1, v2, 1);
            Aresta<string> aresta2 = grafo.InserirAresta(v2, v3, 2);
            Aresta<string> aresta3 = grafo.InserirAresta(v1, v3, 3);
            Aresta<string> aresta4 = grafo.InserirAresta(v1, v4, 4);
            //Vertice<string> verticeSubst = new Vertice<string>("Gallery.htm");
            //grafo.substituirVertice(v1, verticeSubst);
            //bool verticeRemovido = grafo.RemoverVertice(v2);
            Console.WriteLine("Lista de arestas: ");
            List<Aresta<string>> listaArestas = grafo.Arestas();
            foreach (Aresta<string> arestaItem in listaArestas)
            {
                Console.WriteLine(grafo.GetAresta(arestaItem));
                if(!grafo.eDirecionado(arestaItem))
                    Console.WriteLine(arestaItem.to.Value.ToString()+" - "+ arestaItem.from.Value.ToString());
            }
         
            //eAdjacente()
            if (grafo.eAdjacente(v1, v2)) Console.WriteLine(v1.Value.ToString() + " é adjacente de " + v2.Value.ToString());
            else Console.WriteLine(v1.Value.ToString() + " não é adjacente de " + v2.Value.ToString());
            
            /*
            //removerVertice
            //
            //oposto
            Console.WriteLine(grafo.oposto(v1, aresta1).Value.ToString());
            //finalVertice
            Vertice<string>[] verticesFinais = grafo.finalVertices(aresta1);
            Console.WriteLine(verticesFinais[0].Value.ToString()+ " - " +verticesFinais[1].Value.ToString());
            //Subistuir Vertice
            Console.WriteLine("\nNova Lista de Vertices: ");
            Vertice<string> verticeSubst =new Vertice<string>("Galery.htm");
            grafo.substituirVertice(v1, verticeSubst);
            var listaSubstituidos = grafo.Vertices().ToList();
            foreach (Vertice<string> vertice in lista)
            {
                Console.WriteLine(vertice.Value.ToString());
            }
            Aresta<string> arestaSubst = new Aresta<string>(v3, v4);
            grafo.substituirAresta(aresta1, arestaSubst);
            Console.WriteLine("\nNova Lista de Arestas");
            List<Aresta<string>> listaSubst = grafo.Arestas();
            foreach (Aresta<string> arestaItem in listaSubst)
            {
                Console.WriteLine(Aresta<string>.GetAresta(arestaItem));
                if (!grafo.eDirecionado(arestaItem))
                    Console.WriteLine(arestaItem.to.Value.ToString() + " - " + arestaItem.from.Value.ToString());
            }*/
            //Aresta<string> arestaSubst = new Aresta<string>(v3, v4);
            //grafo.substituirAresta(aresta1, arestaSubst);
            
            Console.WriteLine("\nMatriz de Custo");
            grafo.MostrarMatrizdeCusto();
            Console.WriteLine("\nMatriz de Adjacencia");
            grafo.MostrarMatrizdeAdjacencia();
            grafo.eEuleriano();
            //grafo.Dijkstra(v1, v2);
            Console.Read();
        }
    }
}
