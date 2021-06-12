using System;
using System.Collections.Generic;

namespace KAP
{
    public static class ListAdditionalFeatures
    {
        static Random rng = new Random();

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

        #region List<T>.Ind
        /// <summary>
        /// Определяет элемент от любого числа без IndexOutRange
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Ind<T>(this List<T> list, int index)
        {
            index = Math.Abs(index);
            return list[index % list.Count];
        }

        /// <summary>
        /// Определяет элемент от любого числа без IndexOutRange
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Ind<T>(this List<T> list, ref int index, bool shuffle = false)
        {
            if (shuffle && index >= list.Count) list.Shuffle();

            index = Math.Abs(index) % list.Count;
            return list[index];
        }
        #endregion

        public static T Random<T>(this IList<T> list)
        {
            return list[rng.Next(list.Count)];
        }

        public static bool Random<T>(this IList<T> list, out T result)
        {
            if(list.Count == 0)
            {
                result = default;
                return false;
            }
            
            result = list[rng.Next(list.Count)];
            return true;
        }

        public static void Print<T>(this IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                UnityEngine.Debug.Log(list[i]);
            }
        }
    }

}
