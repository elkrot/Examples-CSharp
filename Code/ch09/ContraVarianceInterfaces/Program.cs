using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContraVarianceInterfaces
{
    class Shape
    {
        public int id;
    }

    class Rectangle : Shape
    {

    }

    class ShapeComparer : IComparer<Shape>
    {

        public int Compare(Shape x, Shape y)
        {
            return x.id.CompareTo(y.id);
        }
    }
   
    class Program
    {
        static void Main(string[] args)
        {
            ShapeComparer shapeComparer = new ShapeComparer();
            
            //this is obviously possible
            IComparer<Shape> ic = shapeComparer;

            /* This was not possible until .NET 4,
             * but intuitively it makes sense because
             * anything that accepts Shape should
             * also accept Rectangle.
             */

            IComparer<Rectangle> irc = shapeComparer;

            /* This is not possible because it would imply you can pass
             * a Circle to the comparer, which is obviously wrong
             * when it's really a Rect comparer
             */
            //IComparer<Shape> isc = (IComparer<Shape>)rectComparer;
        }
    }
}
