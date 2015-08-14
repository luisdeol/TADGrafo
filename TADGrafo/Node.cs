using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TADGrafo
{
    public class Node<T>
    {
        private T data;
        private NodeList<T> vizinhos = null;
        public Node() { }
        public Node(T data) : this(data, null) { }
        public Node(T data, NodeList<T> vizinhos)
        {
            this.data = data;
            this.vizinhos = vizinhos;
        }

        public T Value
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        protected NodeList<T> Vizinhos
        {
            get
            {
                return vizinhos;
            }
            set
            {
                vizinhos = value;
            }
        }
    }
}