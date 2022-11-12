namespace Chatroom_Example.Core
{
    public static class ExtensionMethods
    {
        public static List<T> GetPage<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            return source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();
        }
        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
