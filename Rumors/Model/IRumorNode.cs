using System.Collections.Generic;
using Rumors.Networks;

namespace Rumors.Model
{
    interface IRumorNode<TNode,TRumor> : INetworkNode<TNode> where TNode : INetworkNode<TNode> where TRumor : IRumor
    {
        IEnumerable<IRumor> Memory { get; }

        void AddToMemory(IRumor rumor);
    }
}
