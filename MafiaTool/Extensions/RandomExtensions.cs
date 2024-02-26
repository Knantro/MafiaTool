namespace MafiaTool.Extensions; 

public static class RandomExtensions {
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
    {
        return list.OrderBy(x => Guid.NewGuid());
    }
}