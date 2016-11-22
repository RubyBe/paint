using System.Collections.Generic;
using System.Drawing;

namespace Scribble
{
    class Stroke
    {
        public List<PointWithAttributes> points;
        public Color color;
        public Style style;

        public Stroke()
        {

        }

        public enum Style
        {
            Dots,
            Cursives,
            Ellipses
        }
    }
}
