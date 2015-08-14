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
        public GraphNode<T> InserirVertice(GraphNode<T> node)
        {
            nodeSet.Add(node);
            return new GraphNode<T>(node.Value);
        }
        public Aresta<T> InserirAresta(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            listaAresta.Add(new Aresta<T>(from, to, cost));
            to.Neighbors.Add(from);
            from.Neighbors.Add(to);
            return new Aresta<T>(from, to, cost);
        }
        
        public Aresta<T> InserirArestaDirecionada(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            from.Neighbors.Add(to);
            listaAresta.Add(new Aresta<T>(from, to, cost));
            return new Aresta<T>(from, to, cost);
        }
        public bool RemoverVertice(GraphNode<T> node)
        {
            GraphNode<T> nodeToRemove = (GraphNode<T>)nodeSet.FindByValue(node.Value);
            var arestaToRemove = listaAresta.Where(a => a.from == nodeToRemove || a.to == nodeToRemove);
            if (nodeToRemove == null)
                return false;
            nodeSet.Remove(nodeToRemove);
            foreach (Aresta<T> arestaItem in arestaToRemove)
            {
                listaAresta.Remove(arestaItem);
            }
            foreach (GraphNode<T> refNode in nodeSet)
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
        public bool eDirecionado(GraphNode<T> node)
        {
            foreach (GraphNode<T> vertice in node.Neighbors)
            {
                if (!vertice.Neighbors.Contains(node))
                {
                    return true;
                }
                else return false;
            }
            return false;
        }
        public List<Aresta<T>> arestasIncidentes(GraphNode<T> node)
        {
            List<Aresta<T>> lista = new List<Aresta<T>>();
            foreach(Aresta<T> aresta in listaAresta){
                if (aresta.from == node || aresta.to == node)
                    lista.Add(aresta);
            }
            return lista;
        }
        public bool eAdjacente(GraphNode<T> from, GraphNode<T> to)
        {
            return from.Neighbors.Contains(to) || to.Neighbors.Contains(from);
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
