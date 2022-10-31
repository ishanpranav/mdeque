using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MDeque.Tests
{
    [TestClass]
    public class MDequeTest
    {
        private static readonly object[][] s_dynamicData = new object[][]
        {
            new object[]
            {
                Array.Empty<int>()
            },
            new object[]
            {
                new int[] { 0 }
            },
            new object[]
            {
                new int[] { -20, -1, 0, 1, 706 },
            },
            new object[]
            {
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
            },
            new object[]
            {
                new int[] { -1024, -3, -2, -1 }
            }
        };

        public static IEnumerable<object>[] GetDynamicData()
        {
            return s_dynamicData;
        }

        [TestMethod]
        public void TestAddFirst_Null()
        {
            MDeque<object?> list = new MDeque<object?>();

            Assert.ThrowsException<ArgumentNullException>(() => list.AddFirst(item: null));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetDynamicData), DynamicDataSourceType.Method)]
        public void TestAddFirst(int[] data)
        {
            MDeque<int> list = new MDeque<int>();

            foreach (int datum in data)
            {
                list.AddFirst(datum);

                int previous = int.MaxValue;

                foreach (int current in list)
                {
                    Assert.IsTrue(current <= previous);

                    previous = current;
                }
            }

            Assert.AreEqual(data.Length, list.Count);
            Assert.AreEqual(data.Length, list.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetDynamicData), DynamicDataSourceType.Method)]
        public void TestAddLast(int[] data)
        {
            MDeque<int> list = new MDeque<int>();

            foreach (int datum in data)
            {
                list.AddLast(datum);

                int previous = int.MinValue;

                foreach (int current in list)
                {
                    Assert.IsTrue(current >= previous);

                    previous = current;
                }
            }

            Assert.AreEqual(data.Length, list.Count);
            Assert.AreEqual(data.Length, list.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetDynamicData), DynamicDataSourceType.Method)]
        public void TestAdd(int[] data)
        {
            ICollection<int> list1 = new MDeque<int>();
            MDeque<int> list2 = new MDeque<int>();

            foreach (int datum in data)
            {
                list1.Add(datum);
                list2.AddLast(datum);
            }

            using (IEnumerator<int> enumerator1 = list1.GetEnumerator())
            using (IEnumerator<int> enumerator2 = list2.GetEnumerator())
            {
                bool moveNext1;
                bool moveNext2;

                do
                {
                    moveNext1 = enumerator1.MoveNext();
                    moveNext2 = enumerator2.MoveNext();

                    Assert.AreEqual(moveNext1, moveNext2);
                    Assert.AreEqual(enumerator1.Current, enumerator2.Current);
                }
                while (moveNext1 && moveNext2);
            }
        }
    }
}
