using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TADGrafo
{
    public class Vertice<T>: Node<T>
    {
        private List<int> costs;

        public Vertice() : base() { } 
        public Vertice(T value) : base(value) { }
        public Vertice(T value, NodeList<T> neighbors) : base(value, neighbors) { }

        new public NodeList<T> Neighbors
        {
            get
            {
                if (base.Vizinhos == null)
                    base.Vizinhos = new NodeList<T>();

                return base.Vizinhos;
            }
        }

        public List<int> Costs
        {
            get
            {
                if (costs == null)
                    costs = new List<int>();
                return costs;
            }
        }
    }
}
