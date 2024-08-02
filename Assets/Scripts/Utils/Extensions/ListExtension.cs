using System.Collections.Generic;
using UnityEngine;

namespace Utils.Extensions
{
    public static class ListExtension 
    {
        /// <summary>
        /// Shuffles the element order in list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> listToShuffle) {
            var count = listToShuffle.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) {
                var randomIndex = Random.Range(i, count);
                (listToShuffle[i], listToShuffle[randomIndex]) = (listToShuffle[randomIndex], listToShuffle[i]);
            }
        }
    }
}
