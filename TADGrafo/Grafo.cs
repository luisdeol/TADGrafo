using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TADGrafo
{
    public class Graph<T> :IEnumerable<T>
    {
        protected NodeList<T> verticeSet;
        protected List<Aresta<T>> arestaList;
        protected Collection<T> coleçao;
        public Graph() : this(null) { }
        public Graph(NodeList<T> verticeSet)
        {
            if (verticeSet == null)
            {
                this.verticeSet = new NodeList<T>();
                this.arestaList = new List<Aresta<T>>();
            }
            else
                this.verticeSet = verticeSet;
        }
        public Vertice<T> InserirVertice(Vertice<T> vertice)
        {
            verticeSet.Add(vertice);
            return new Vertice<T>(vertice.Value);
        }
        public Aresta<T> InserirAresta(Vertice<T> from, Vertice<T> to, int cost)
        {
            arestaList.Add(new Aresta<T>(from, to, cost));
            to.Neighbors.Add(from);
            from.Neighbors.Add(to);
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
            Vertice<T> nodeToRemove = (Vertice<T>)verticeSet.FindByValue(vertice.Value);
            var arestaToRemove = arestaList.Where(a => a.from == nodeToRemove || a.to == nodeToRemove);
            if (nodeToRemove == null)
                return false;
            verticeSet.Remove(nodeToRemove);
            foreach (Aresta<T> arestaItem in arestaToRemove)
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
        public NodeList<T> Vertices()
        {
            return verticeSet;
        }
        public List<Aresta<T>> Arestas()
        {
            return arestaList;
        }
        public bool eDirecionado(Aresta<T> aresta)
        {
            if((aresta.from.Neighbors.Contains(aresta.to))&&(aresta.to.Neighbors.Contains(aresta.from)))
                return false;
            else return true;
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
            Vertice<T> vertice = (Vertice<T>)verticeSet.FindByValue(antigoVertice.Value);
            vertice.Value = novoVertice.Value;
        }
        public void substituirAresta(Aresta<T> antigaAresta, Aresta<T> novaAresta)
        {
            int index = arestaList.FindIndex(a=>a.from==antigaAresta.from && a.to==antigaAresta.to);
            if (index > -1)
                arestaList[index] = novaAresta;
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
