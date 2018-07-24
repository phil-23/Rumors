using System.Collections.Generic;

namespace Rumors.Networks
{
    interface INetworkNode<TNode> where TNode : INetworkNode<TNode>
    {
        IEnumerable<TNode> Neighbors { get; }

        void AddNeighbor(TNode neighbor);
    }
}
