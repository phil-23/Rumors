using System.Collections.Generic;
using Rumors.Networks;

namespace Rumors.Model
{
    interface IRumorNode<T> : INetworkNode<T> where T : IRumorNode<T>
    {
        IEnumerable<Rumor> Memory { get; }

        void AddToMemory(Rumor rumor);
    }
}
