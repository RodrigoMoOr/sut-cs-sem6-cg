using System;
using System.Collections.Generic;
public static class RandomValues
{
    public static Random random = new Random();

    public static T PickRandom<T>(this IList<T> source)
    {
        if (source.Count > 0)
        {
            int randIndex = random.Next(source.Count);
            return source[randIndex];
        }
        else return default(T);
    }

}
