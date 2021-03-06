﻿using PoemsModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PoemsModule.DataAccessLayer
{
    public class PoemsService
    {
        private readonly Random rnd = new Random();

        public double GetDistance(string text)
        {
            string[] sentences = text.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            double maxCost = 0, curCost = 0;
            if (sentences.Length > 1)
            {
                for (int i = 0; i < sentences.Length - 1; i++)
                {
                    curCost = JaroWinklerDistance.Distance(sentences[i], sentences[i + 1]);
                    if (maxCost < curCost) maxCost = curCost;
                }
            }
            else if (sentences.Length == 1) maxCost = JaroWinklerDistance.Distance(sentences[0], "");
            return maxCost;
        }

        /// <summary> Конвертирование JSON в Poem </summary>
        /// <param name="jsonString">JSON</param>
        /// <returns>object || null</returns>
        public Poem MappingPoem(string jsonString)
        {
            Poem poem = null;
            IList<Poem> poems = JsonSerializer.Deserialize<List<Poem>>(jsonString);
            // по заданию нужно вставлять одну поэму, а приходит arr[] => выбираем любую
            if (poems != null && poems.Count > 0)
            {
                poem = poems[rnd.Next(0, poems.Count - 1)];
                poem.Content = poem.Content;
            }
            return poem;
        }
        // алгоритм Jaro–Winkler distance
        private static class JaroWinklerDistance
        {
            private static readonly double mWeightThreshold = 0.7;
            private static readonly int mNumChars = 4;

            public static double Distance(string aString1, string aString2)
            {
                return 1.0 - Proximity(aString1, aString2);
            }

            public static double Proximity(string aString1, string aString2)
            {
                int lLen1 = aString1.Length;
                int lLen2 = aString2.Length;
                if (lLen1 == 0)
                    return lLen2 == 0 ? 1.0 : 0.0;

                int lSearchRange = Math.Max(0, Math.Max(lLen1, lLen2) / 2 - 1);

                bool[] lMatched1 = new bool[lLen1];
                bool[] lMatched2 = new bool[lLen2];

                int lNumCommon = 0;
                for (int i = 0; i < lLen1; ++i)
                {
                    int lStart = Math.Max(0, i - lSearchRange);
                    int lEnd = Math.Min(i + lSearchRange + 1, lLen2);
                    for (int j = lStart; j < lEnd; ++j)
                    {
                        if (lMatched2[j]) continue;
                        if (aString1[i] != aString2[j])
                            continue;
                        lMatched1[i] = true;
                        lMatched2[j] = true;
                        ++lNumCommon;
                        break;
                    }
                }
                if (lNumCommon == 0) return 0.0;

                int lNumHalfTransposed = 0;
                int k = 0;
                for (int i = 0; i < lLen1; ++i)
                {
                    if (!lMatched1[i]) continue;
                    while (!lMatched2[k]) ++k;
                    if (aString1[i] != aString2[k])
                        ++lNumHalfTransposed;
                    ++k;
                }

                int lNumTransposed = lNumHalfTransposed / 2;

                double lNumCommonD = lNumCommon;
                double lWeight = (lNumCommonD / lLen1
                                 + lNumCommonD / lLen2
                                 + (lNumCommon - lNumTransposed) / lNumCommonD) / 3.0;

                if (lWeight <= mWeightThreshold) return lWeight;
                int lMax = Math.Min(mNumChars, Math.Min(aString1.Length, aString2.Length));
                int lPos = 0;
                while (lPos < lMax && aString1[lPos] == aString2[lPos])
                    ++lPos;
                if (lPos == 0) return lWeight;
                return lWeight + 0.1 * lPos * (1.0 - lWeight);

            }
        }
    }
}