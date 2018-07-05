using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContraVarianceDelegates
{
    class Shape
    {
        public void Draw() { Console.WriteLine("Drawing shape"); }
    };

    class Rectangle : Shape
    {
        public void Expand() { /*...*/ }
    };

    class Program
    {
        delegate void ShapeAction<in T>(T shape);

        static void DrawShape(Shape shape)
        {
            if (shape != null)
            {
                shape.Draw();
            }
        }

        static void Main(string[] args)
        {
            //this should obviously be ok
            ShapeAction<Shape> action = DrawShape;
            action(new Rectangle());

            /* Intuitively, you know any method that
             * conforms to a ShapeAction<Shape> delegate
             * should also work on a Rectangle because
             * Rectangle is a type of Shape.
             * 
             * It's always possible to assign a less derived _method_
             * to a more-derived delegate, but until .NET 4
             * you could not assign a less-derived _delegate_ to
             * a more-derived delegate: an important distinction.
             * 
             * Now, as long as the type parameter is marked as "in"
             * you can.
             */

            //this was possible before .NET 4
            ShapeAction<Rectangle> rectAction1 = DrawShape;
            rectAction1(new Rectangle());

            //this was NOT possible before .NET 4
            ShapeAction<Rectangle> rectAction2 = action;
            rectAction2(new Rectangle());

            Console.ReadKey();
        }
    }
}

