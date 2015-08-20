using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TADGrafo
{
    public class Grafo<T> :IEnumerable<T>
    {
        const int size=100;
        int[,] matrizDeCusto = new int[size, size];
        int[,] matrizDeAdjacencia = new int[size, size];
        protected List<Vertice<T>> verticeSet;
        protected List<Aresta<T>> arestaList;
        protected Collection<T> coleçao;
        public Grafo() : this(null) { }
        public Grafo(List<Vertice<T>> _verticeSet)
        {
            if (_verticeSet == null)
            {
                this.verticeSet = new List<Vertice<T>>();
                this.arestaList = new List<Aresta<T>>();
            }
            else
                this.verticeSet = _verticeSet;
        }
        public Vertice<T> InserirVertice(Vertice<T> vertice)
        {
            verticeSet.Add(vertice);
            return new Vertice<T>(vertice.Value);
        }
        public Aresta<T> InserirAresta(Vertice<T> from, Vertice<T> to, int cost)
        {
            to.Neighbors.Add(from);
            from.Neighbors.Add(to);
            arestaList.Add(new Aresta<T>(from, to, cost));
            return new Aresta<T>(from, to, cost);
        }
        
        public Aresta<T> InserirArestaDirecionada(Vertice<T> from, Vertice<T> to, int cost)
        {
            from.Neighbors.Add(to);
            arestaList.Add(new Aresta<T>(from, to, cost));
            return new Aresta<T>(from, to, cost);
        }
        public bool RemoverVertice(Vertice<T> vertice)
        {
            Vertice<T> nodeToRemove = FindByValue(vertice.Value);
            IEnumerable<Aresta<T>> arestaToRemove = arestaList.Where(a => a.from == nodeToRemove || a.to == nodeToRemove);
            var selectedA = new List<Aresta<T>>();
            if (nodeToRemove == null)
                return false;
            verticeSet.Remove(nodeToRemove);
            foreach (Aresta<T> arestaItem in arestaToRemove)
            {
                selectedA.Add(arestaItem);
            }
            foreach (Aresta<T> arestaItem in selectedA)
            {
                arestaList.Remove(arestaItem);
            }
            foreach (Vertice<T> refNode in verticeSet)
            {
                int index = refNode.Neighbors.IndexOf(nodeToRemove);
                if (index != -1)
                {
                   refNode.Neighbors.RemoveAt(index);
                   //refNode.Costs.RemoveAt(index);
                }
            }
            return true;
        }
        
        public bool eDirecionado(Aresta<T> aresta)
        {
            try
            {
                bool duplaDirecao = (aresta.from.Neighbors.Contains(aresta.to)) && (aresta.to.Neighbors.Contains(aresta.from));
                if(duplaDirecao)
                return false;
                return true;
            }
            catch { return true; }
        }
        public List<Aresta<T>> arestasIncidentes(Vertice<T> node)
        {
            List<Aresta<T>> lista = new List<Aresta<T>>();
            foreach(Aresta<T> aresta in arestaList){
                if (aresta.from == node || aresta.to == node)
                    lista.Add(aresta);
            }
            return lista;
        }
        public Vertice<T> oposto(Vertice<T> vertice, Aresta<T> aresta)
        {
            if (eAdjacente(vertice, aresta.from))
                return aresta.from;
            else if (eAdjacente(vertice, aresta.to))
                return aresta.to;
            else throw new Exception { };
        }
        public bool eAdjacente(Vertice<T> from, Vertice<T> to)
        {
            return from.Neighbors.Contains(to) || to.Neighbors.Contains(from);
        }

        public Vertice<T>[] finalVertices(Aresta<T> aresta)
        {
            return new Vertice<T>[2] { aresta.from, aresta.to};
        }
        public void substituirVertice(Vertice<T> antigoVertice, Vertice<T> novoVertice)
        {
            Vertice<T> vertice = FindByValue(antigoVertice.Value);
            vertice.Value = novoVertice.Value;
        }
        public void substituirAresta(Aresta<T> antigaAresta, Aresta<T> novaAresta)
        {
            int index = arestaList.FindIndex(a=>a.from==antigaAresta.from && a.to==antigaAresta.to);
            if (index > -1)
                arestaList[index] = novaAresta;
        }
        public Vertice<T> FindByValue(T value)
        {
            return verticeSet.FirstOrDefault(v => v.Value.Equals(value));
        }
        public List<Vertice<T>> Vertices()
        {
            return verticeSet;
        }
        public List<Aresta<T>> Arestas()
        {
            return arestaList;
        }
        public void MostrarMatrizdeCusto()
        {
            List<Vertice<T>> listaVertice = Vertices();
            List<Aresta<T>> listaAresta = arestaList;
            
            for (int i = 0; i < listaVertice.Count; i++) {
                for (int j = 0; j < listaVertice.Count; j++)
                {
                    if (arestaList.Find(a => a.from == listaVertice.ElementAt(i) && a.to == listaVertice.ElementAt(j)) != null)
                    {
                        int custo = arestaList.Find(a => a.from == listaVertice.ElementAt(i) && a.to == listaVertice.ElementAt(j)).Cost;
                        matrizDeCusto[i, j] = custo;
                    }
                    else if (eDirecionado(arestaList.Find(a => a.from == listaVertice.ElementAt(j) && a.to == listaVertice.ElementAt(i)))==false)
                   {
                       int custo = arestaList.Find(a => a.from == listaVertice.ElementAt(j) && a.to == listaVertice.ElementAt(i)).Cost;
                        matrizDeCusto[i, j] = custo;
                    }
                    else { matrizDeCusto[i, j] = 0;
                    }
                    Console.Write(matrizDeCusto[i, j].ToString() + " ");
                }
                Console.WriteLine("\n");
            }
        }
        public void MostrarMatrizdeAdjacencia()
        {
            List<Vertice<T>> listaVertice = Vertices();
            List<Aresta<T>> listaAresta = arestaList;
            for (int i = 0; i < listaVertice.Count; i++)
            {
                for (int j = 0; j < listaVertice.Count; j++)
                {
                    if (arestaList.Find(a => a.from == listaVertice.ElementAt(i) && a.to == listaVertice.ElementAt(j)) != null)
                    {
                        matrizDeAdjacencia[i, j] = 1;
                    }
                    else if (eDirecionado(arestaList.Find(a => a.from == listaVertice.ElementAt(j) && a.to == listaVertice.ElementAt(i))) == false)
                    {
                        matrizDeAdjacencia[i, j] = 1;
                    }
                    else
                    {
                        matrizDeAdjacencia[i, j] = 0;
                    }
                    Console.Write(matrizDeAdjacencia[i, j].ToString() + " ");
                }
                Console.WriteLine("\n");
            }
        }
        public string GetAresta(Aresta<T> aresta)
        {
            try
            {
                return aresta.from.Value.ToString() + " - " + aresta.to.Value.ToString();
            }
            catch
            {
                return "";
            }
        }
        
        public void eEuleriano()
        {
            int soma=0;
            int grau;
            int linhaAtual=0;
            int numeroLinhas=verticeSet.Count;
            while((soma<=2)&&(linhaAtual<numeroLinhas)){
                grau = 0;
                for (int i = 0; i < numeroLinhas; i++)
                {
                    grau += matrizDeAdjacencia[linhaAtual, i];
                }
                if (grau % 2 == 1)
                {
                    soma++;
                }
                Console.WriteLine(grau.ToString());
                linhaAtual++;
            }
            if (soma > 2)
                Console.WriteLine("Nao é Euleriano!");
            else if (soma == 2)
                Console.WriteLine("É semieuleriano!");
            else
                Console.WriteLine("É euleriano!");
        }
        public void Dijkstra(Vertice<T> from, Vertice<T> to)
        {
            List<Vertice<T>> listaNaoVisitados = new List<Vertice<T>>();
            List<Aresta<T>> arestas = Arestas();
            string path = "";
            int custo = 0;
            bool Eprimeiro = true;
            foreach (Vertice<T> v in verticeSet)
            {
                listaNaoVisitados.Add(v);
            }
            foreach (Aresta<T> a in arestas)
            {
                a.Cost = int.MaxValue;

            }
            arestas.ElementAt(0).Cost = 0;
            listaNaoVisitados.Remove(from);
            while (listaNaoVisitados.Count != 0)
            {
                int smallest = 0;
                Vertice<T> visitado = new Vertice<T>();
                Aresta<T> arestavisitada = new Aresta<T>(null, null);
                Aresta<T> arestavisitadaFinalWhile = new Aresta<T>(null, null);

                foreach (Vertice<T> v in from.Neighbors)
                {
                    if (Eprimeiro)
                    {
                        if (arestas.Find(a => a.from == from && a.to == v).Cost < smallest)
                        {
                            smallest = arestas.Find(a => a.from == from && a.to == v).Cost;
                            visitado = arestas.Find(a => a.from == from && a.to == v).to;
                            arestavisitada = arestas.Find(a => a.from == from && a.to == v);
                        }
                    }
                    else
                    {
                        if (arestas.Find(a => a.from == from && a.to == v).Cost + arestavisitadaFinalWhile.Cost < arestas.Find(a => a.from == arestavisitadaFinalWhile.to && a.to == v).Cost)
                        {
                            smallest = arestas.Find(a => a.from == from && a.to == v).Cost;
                            visitado = arestas.Find(a => a.from == from && a.to == v).to;
                            arestavisitada = arestas.Find(a => a.from == from && a.to == v);
                        }
                    }
                }
                arestavisitadaFinalWhile = arestavisitada;
                path = path + GetAresta(arestavisitadaFinalWhile).ToString();
                custo = custo + arestavisitada.Cost;
                listaNaoVisitados.Remove(from);
                from = visitado;
                Eprimeiro = false;
            }
            Console.WriteLine(path);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return coleçao.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
