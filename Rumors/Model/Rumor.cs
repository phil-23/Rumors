using System;

namespace Rumors.Model
{
    struct Rumor
    {
        public const int INFORMATION_SPACE = 5;

        public readonly Guid Type;
        public readonly int Information;

        public bool this[int bit] => (Information & (1 << bit)) != 0;

        public Rumor(Guid type, int info)
        {
            Type = type;
            Information = info;
        }

        public Rumor(Rumor parent, Random random, bool mutate)
        {
            Type = parent.Type;
            Information = mutate ? Mutate(parent.Information, random) : parent.Information;
        }

        private static int Mutate(int input, Random random)
        {
            int bit = random.Next(INFORMATION_SPACE);
            return input ^ (1 << bit);
        }
    }
}
