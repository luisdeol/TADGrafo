using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TADGrafo
{
    public class Aresta<T>
    {
        public GraphNode<T> from { get; set; }
        public GraphNode<T> to {get;set;}
        public int Cost { get; set; }
        public Aresta(GraphNode<T> from, GraphNode<T> to, int cost=0){
            this.from = from;
            this.to = to;
            this.Cost = cost;
            }
        static public string GetAresta(Aresta<T> aresta)
        {
            return aresta.from.Value.ToString() + " - " + aresta.to.Value.ToString();
        }
    }
}
