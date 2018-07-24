using System;
using System.Collections.Generic;
using System.Linq;
using Rumors.Utilities;

namespace Rumors.Networks
{
    /// <summary>
    /// Factory for Barabási–Albert scale-free networks.
    /// See https://en.wikipedia.org/wiki/Barabási–Albert_model
    /// </summary>
    static class BANetwork
    {
        /// <summary>
        /// Creates a Barabási–Albert scale-free network.
        /// </summary>
        /// <typeparam name="T">The node type; must implement INetworkNode</typeparam>
        /// <param name="nodeCount">Total number of nodes in the network</param>
        /// <param name="m0">Size of initial set of linked nodes</param>
        /// <param name="m">How many nodes each new node links to</param>
        public static IEnumerable<T> Create<T>(int nodeCount, int m0, int m, Func<T> factory, Random random) where T : INetworkNode<T>
        {
            var nodes = CreateInitial(m0, factory);
            while(nodes.Count < nodeCount)
            {
                nodes.Add(CreateNode(nodes, m, factory, random));
            }
            return nodes;
        }

        private static List<T> CreateInitial<T>(int m0, Func<T> factory) where T : INetworkNode<T>
        {
            var nodes = new List<T>();

            for(int i = 0; i < m0; i++)
            {
                nodes.Add(factory());
                for(int j = 0; j < i; j++)
                {
                    Link(nodes[i], nodes[j]);
                }
            }

            return nodes;
        }

        private static T CreateNode<T>(List<T> nodes, int m, Func<T> factory, Random random) where T : INetworkNode<T>
        {
            var newNode = factory();
            var linkedNodes = nodes.WeightedRandom(x => x.Neighbors.Count(), random, m);
            foreach(var node in linkedNodes)
            {
                Link(newNode, node);
            }
            return newNode;
        }

        private static void Link<T>(T node1, T node2) where T : INetworkNode<T>
        {
            node1.AddNeighbor(node2);
            node2.AddNeighbor(node1);
        }
    }
}
