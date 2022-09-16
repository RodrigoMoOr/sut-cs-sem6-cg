using System;
using System.Collections.Generic;
public static class RandomValues
{
    private static Random random = new();

    public static Random Random { get => random; set => random = value; }

    public static T PickRandom<T>(this IList<T> source)
    {
        if (source.Count > 0)
        {
            int randIndex = Random.Next(source.Count);
            return source[randIndex];
        }
        else return default(T);
    }

}
