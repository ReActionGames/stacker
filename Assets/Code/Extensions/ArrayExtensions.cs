using UnityEngine;

namespace HenderStudios.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Gets a random element from the given array.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array in which to pick an element.</param>
        /// <returns>A random element from this array.</returns>
        public static T PickRandom<T>(this T[] array)
        {
            int index;
            index = Random.Range(0, array.Length);
            return array[index];
        }

        /// <summary>
        /// Randomly shuffles a given array.
        /// </summary>
        /// <typeparam name="T">The type of array.</typeparam>
        /// <param name="deck">The array calling this method.</param>
        /// <returns>An identical array, but with the elements randomly shuffled.</returns>
        public static T[] Shuffle<T>(this T[] deck)
        {
            for (int i = 0; i < deck.Length; i++)
            {
                T temp = deck[i];
                int randomIndex = Random.Range(0, deck.Length);
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
            return deck;
        }
    }
}