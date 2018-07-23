using System.Collections.Generic;

namespace Rumors.Networks
{
    interface INetworkNode<T> where T : INetworkNode<T>
    {
        IList<T> Neighbors { get; } 
    }
}
