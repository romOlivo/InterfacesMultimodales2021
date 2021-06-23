using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiimoteGestureLib
{
    public class Gesture
    {
        private List<double[]> samples;

        public string Name { get; set; }

        public Gesture(string name = "Unrecognized")
        {
            Name = name;
            samples = new List<double[]>();
        }

        public IReadOnlyList<double[]> GetSamples()
        {
            return samples;
        }

        public void AddSample(double x, double y, double z)
        {
            samples.Add(new double[3] { x, y, z });
        }

        public double DistanceTo(Gesture other)
        {
            var g = samples;
            var h = other.GetSamples();
            double[,] mem = new double[g.Count, h.Count];

            double D(int i, int j)
            {
                if (mem[i, j] == -1)
                {
                    if (i == 0 && j == 0)
                        mem[0, 0] = 2 * d(g[0], h[0]);
                    else if (j == 0)
                        mem[i, 0] = D(i - 1, 0) + d(g[i], h[0]);
                    else if (i == 0)
                        mem[0, j] = D(0, j - 1) + d(g[0], h[j]);
                    else
                    {
                        var de = d(g[i], h[j]);
                        var d1 = D(i-1, j);
                        var d2 = D(i, j-1);
                        var d3 = D(i-1, j-1) + de;
                        mem[i, j] = de + Math.Min(d1, Math.Min(d2, d3));
                    }
                }
                return mem[i, j];
            }
            for (int k = 0; k < g.Count; k++) for (int l = 1; l < h.Count; l++) mem[k, l] = -1;
            return D(g.Count-1, h.Count-1);
        }

        static double d(double[] g, double[] h)
        {
            var d0 = g[0] - h[0];
            var d1 = g[1] - h[1];
            var d2 = g[2] - h[2];
            return Math.Sqrt(d0*d0 + d1*d1 + d2*d2);
        }


    }
}
