using System;
using System.Collections.Generic;
using System.Linq;

namespace Rumors.Model
{
    static class RumorNodeExtensions
    {
        public static IDictionary<Rumor,double> InformationFrequency<T> (this IRumorNode<T> node) where T : IRumorNode<T>
        {
            int current;
            var counts = new Dictionary<Rumor, int>();

            foreach(var rumor in node.Memory)
            {
                if(counts.TryGetValue(rumor, out current))
                {
                    counts[rumor] = current + 1;
                }
                else
                {
                    counts[rumor] = 0;
                }
            }

            var total = (double)counts.Sum(x => x.Value);
            return counts.ToDictionary(x => x.Key, x => (double)x.Value / total);
        }

        public static double ShannonEntropy<T>(this IRumorNode<T> node) where T : IRumorNode<T>
        {
            var f = node.InformationFrequency();
            return -f.Sum(fi => fi.Value * Math.Log(fi.Value, 2));
        }

        public static double DistortionChance<T>(this IRumorNode<T> node, float K) where T : IRumorNode<T>
        {
            var Hn = node.ShannonEntropy();
            var Hmax = Math.Log(1.0 / Rumor.INFORMATION_SPACE, 2);
            return 1.0 / (Math.Exp(K * (Hmax - Hn) / Hmax) + 1.0);
        }        
    }
}
