using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RussianLotto.Tests
{
    public static class StringHelp
    {
        public static string EvenColumns(int desiredWidth, IEnumerable<IEnumerable<string>> lists) {
            return string.Join(Environment.NewLine, EvenColumns(desiredWidth, true, lists));
        }

        public static IEnumerable<string> EvenColumns(int desiredWidth, bool rightOrLeft, IEnumerable<IEnumerable<string>> lists) {
            return lists.Select(o => EvenColumns(desiredWidth, rightOrLeft, o.ToArray()));
        }

        public static string EvenColumns(int desiredWidth, bool rightOrLeftAlignment, string[] list, bool fitToItems = false) {
            // right alignment needs "-X" 'width' vs left alignment which is just "X" in the `string.Format` format string
            int columnWidth = (rightOrLeftAlignment ? -1 : 1) *
                              // fit to actual items? this could screw up "evenness" if
                              // one column is longer than the others
                              // and you use this with multiple rows
                              (fitToItems
                                  ? Mathf.Max(desiredWidth, list.Select(o => o.Length).Max())
                                  : desiredWidth
                              );

            // make columns for all but the "last" (or first) one
            string format = string.Concat(Enumerable.Range(rightOrLeftAlignment ? 0 : 1, list.Length-1).Select( i => string.Format("{{{0},{1}}}", i, columnWidth) ));

            // then add the "last" one without Alignment
            if(rightOrLeftAlignment) {
                format += "{" + (list.Length-1) + "}";
            }
            else {
                format = "{0}" + format;
            }

            return string.Format(format, list);
        }

        public static T[][] ToJaggedArray<T>(T[,] multiArray) {
            var h = multiArray.GetLength(0);
            var w = multiArray.GetLength(1);

            var result = new T[h][];
            for (var r = 0; r < h; r++) {
                result[r] = new T[w];
                for (var c = 0; c < w; c++) {
                    result[r][c] = multiArray[r, c];
                }
            }
            return result;
        }
    }
}