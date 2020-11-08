using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TombolaTicketGenerator
{
    internal class clsTicketGenerator
    {
        internal static Dictionary<int, int[]> getTicketNumbersColumnWise()
        {
            List<int> listAllottedNumbers = new List<int>();

            Dictionary<int, int[]> dic = new Dictionary<int, int[]>();

            for (int i = 1; i <= 9; i++)
            {
                int[] nums = new int[]
                {
                    getNextNumber(i, listAllottedNumbers),
                    getNextNumber(i, listAllottedNumbers),
                    getNextNumber(i, listAllottedNumbers)
                };

                Array.Sort(nums);

                dic[i] = nums;
            }

            return dic;
        }

        internal static Dictionary<int, int[]> getNumberPositionsByRow()
        {
            Dictionary<int, int[]> dicRowNumberPlaces = new Dictionary<int, int[]>();

            List<int> one = getFiveNumberPositionsForRow();
            List<int> two = getFiveNumberPositionsForRow();
            List<int> three = getFiveNumberPositionsForRow();

            List<int> temp = Enumerable.Range(1, 9).ToList();

            List<int> notfound = temp.Except(one).Except(two).Except(three).ToList();

            List<int> duplicates = one.Intersect(two).Intersect(three).ToList();

            foreach (int not in notfound)
            {
                foreach (int d in duplicates)
                {
                    if (one.Contains(d) && !one.Contains(not))
                    {
                        one.Remove(d);
                        one.Add(not);
                        break;
                    }

                    if (two.Contains(d) && !two.Contains(not))
                    {
                        two.Remove(d);
                        two.Add(not);
                        break;
                    }
                }
            }

            one.Sort();
            two.Sort();
            three.Sort();

            dicRowNumberPlaces.Add(1, one.ToArray());
            dicRowNumberPlaces.Add(2, two.ToArray());
            dicRowNumberPlaces.Add(3, three.ToArray());

            return dicRowNumberPlaces;
        }

        static int getNextNumber(int c, List<int> listAllottedNumbers)
        {
            int startnum = (c - 1) * 10;

            if (startnum == 0) startnum = 1;

            int endnum = (c * 10) - 1;

            if (endnum == 89) endnum = 90;

            //int num = random.Next(startnum, endnum + 1);
            int num = clsRandom.Instance.Next(startnum, endnum + 1);

            if (listAllottedNumbers.Contains(num))
            {
                num = getNextNumber(c, listAllottedNumbers);
            }

            if (!listAllottedNumbers.Contains(num))
            {
                listAllottedNumbers.Add(num);
            }

            return num;
        }

        static List<int> getFiveNumberPositionsForRow()
        {
            List<int> dic1 = new List<int>();

            for (int i = 1; i <= 5; i++)
            {
                dic1.Add(getNextNumberPosition(dic1));

                dic1.Sort();
            }

            return dic1;
        }

        static int getNextNumberPosition(List<int> dic)
        {
            //int c = random.Next(1, 10);
            int c = clsRandom.Instance.Next(1, 10);

            if (dic.Contains(c))
            {
                c = getNextNumberPosition(dic);
            }

            return c;
        }

        
    }
}
