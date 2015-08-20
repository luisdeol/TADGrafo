using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TADGrafo
{
    public class Vertice<T>: Node<T>
    {

        public Vertice() : base() { } 
        public Vertice(T value) : base(value) { }
        public Vertice(T value, NodeList<T> neighbors) : base(value, neighbors) { }

        public NodeList<T> Neighbors
        {
            get
            {
                if (base.Vizinhos == null)
                    base.Vizinhos = new NodeList<T>();

                return base.Vizinhos;
            }
        }

        
    }
}
