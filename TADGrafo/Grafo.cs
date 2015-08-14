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
        protected NodeList<T> nodeSet;
        protected List<Aresta<T>> listaAresta;
        protected Collection<T> coleçao;
        public Graph() : this(null) { }
        public Graph(NodeList<T> nodeSet)
        {
            if (nodeSet == null)
            {
                this.nodeSet = new NodeList<T>();
                this.listaAresta = new List<Aresta<T>>();
            }
            else
                this.nodeSet = nodeSet;
        }
        public Vertice<T> InserirVertice(Vertice<T> node)
        {
            nodeSet.Add(node);
            return new Vertice<T>(node.Value);
        }
        public Aresta<T> InserirAresta(Vertice<T> from, Vertice<T> to, int cost)
        {
            listaAresta.Add(new Aresta<T>(from, to, cost));
            to.Neighbors.Add(from);
            from.Neighbors.Add(to);
            return new Aresta<T>(from, to, cost);
        }
        
        public Aresta<T> InserirArestaDirecionada(Vertice<T> from, Vertice<T> to, int cost)
        {
            from.Neighbors.Add(to);
            listaAresta.Add(new Aresta<T>(from, to, cost));
            return new Aresta<T>(from, to, cost);
        }
        public bool RemoverVertice(Vertice<T> node)
        {
            Vertice<T> nodeToRemove = (Vertice<T>)nodeSet.FindByValue(node.Value);
            var arestaToRemove = listaAresta.Where(a => a.from == nodeToRemove || a.to == nodeToRemove);
            if (nodeToRemove == null)
                return false;
            nodeSet.Remove(nodeToRemove);
            foreach (Aresta<T> arestaItem in arestaToRemove)
            {
                listaAresta.Remove(arestaItem);
            }
            foreach (Vertice<T> refNode in nodeSet)
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
            return nodeSet;
        }
        public List<Aresta<T>> Arestas()
        {
            return listaAresta;
        }
        public bool eDirecionado(Vertice<T> node)
        {
            foreach (Vertice<T> vertice in node.Neighbors)
            {
                if (!vertice.Neighbors.Contains(node))
                {
                    return true;
                }
                else return false;
            }
            return false;
        }
        public List<Aresta<T>> arestasIncidentes(Vertice<T> node)
        {
            List<Aresta<T>> lista = new List<Aresta<T>>();
            foreach(Aresta<T> aresta in listaAresta){
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
