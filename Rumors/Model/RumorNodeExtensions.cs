using Rumors.Networks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rumors.Model
{
    static class RumorNodeExtensions
    {
        public static IDictionary<IRumor, double> InformationFrequency<TNode,TRumor>(this IRumorNode<TNode, TRumor> node) where TNode : INetworkNode<TNode> where TRumor : IRumor
        {
            int current;
            var counts = new Dictionary<IRumor, int>();

            foreach (var rumor in node.Memory)
            {
                if (counts.TryGetValue(rumor, out current))
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

        public static double DistortionChance<TNode,TRumor>(this IRumorNode<TNode,TRumor> node, float K) where TNode : INetworkNode<TNode> where TRumor : IRumor
        {
            var Hn = Entropy<TNode,TRumor>.ShannonEntropy(node);
            var Hmax = Entropy<TNode, TRumor>.MaxEntropy(node);
            return 1.0 / (Math.Exp(K * (Hmax - Hn) / Hmax) + 1.0);
        }     
        
        private static class Entropy<TNode,TRumor> where TNode : INetworkNode<TNode> where TRumor : IRumor
        {
            public static double ShannonEntropy(IRumorNode<TNode, TRumor> node)
            {
                var f = node.InformationFrequency();
                return -f.Sum(fi => fi.Value * Math.Log(fi.Value, 2));
            }

            public static double MaxEntropy(IRumorNode<TNode, TRumor> node)
            {
                if(!node.Memory.Any())
                {
                    return 0.0;
                }
                return Math.Log(1.0 / node.Memory.First().InformationSpace, 2);
            }
        }
    }
}
