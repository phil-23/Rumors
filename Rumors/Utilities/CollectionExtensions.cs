using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rumors.Utilities
{
    static class CollectionExtensions
    {
        /// <summary>
        /// Choose unique items by weighted random selection.
        /// </summary>
        public static IEnumerable<T> WeightedRandom<T>(this IEnumerable<T> items, Func<T,int> weight, Random random, int count = 1)
        {
            if(count > items.Count())
            {
                throw new ArgumentException($"WeightedRandom: items = {items.Count()}, count = {count}");
            }

            int total = items.Sum(weight);
            var picked = new HashSet<T>();
            while(picked.Count < count)
            {
                int choice = random.Next(total);
                picked.Add(WeightedRandomPick(items, weight, choice));
            }

            return picked;
        }

        private static T WeightedRandomPick<T>(IEnumerable<T> items, Func<T,int> weight, int choice)
        {
            foreach (var item in items)
            {
                choice -= weight(item);
                if (choice < 0)
                {
                    return item;
                }
            }

            throw new IndexOutOfRangeException($"WeightedRandom: items = {items.Count()}");
        }
    }
}
