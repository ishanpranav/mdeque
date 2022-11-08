using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MDeque.Tests
{
    [TestClass]
    public class MDequeTest
    {
        private readonly MDeque<int> _mDeque = new MDeque<int>();

        private void Initialize(int[] items)
        {
            ICollection<int> collection = _mDeque;

            foreach (int item in items)
            {
                collection.Add(item);
            }
        }

        [DataRow("[0]", 0, new int[0])]
        [DataRow("[1, 2, 3]", 1, new int[] { 2, 3 })]
        [DataRow("[1, 2, 4, 8]", 1, new int[] { 2, 4, 8 })]
        [DataRow("[-5, -4, -3, -2]", -5, new int[] { -4, -3, -2 })]
        [DataRow("[2, 4, 6, 8]", 2, new int[] { 4, 6, 8 })]
        [DataTestMethod]
        public void TestAddFirst(string expected, int item, int[] items)
        {
            Initialize(items);

            _mDeque.AddFirst(item);

            Assert.AreEqual(expected, _mDeque.ToString());
        }

        [DataRow("[1]", 1, new int[0])]
        [DataRow("[1, 2]", 2, new int[] { 1 })]
        [DataRow("[1, 2, 3]", 2, new int[] { 1, 3 })]
        [DataRow("[1, 2, 3, 4, 5]", 3, new int[] { 1, 2, 4, 5 })]
        [DataRow("[1, 2, 3, 4, 5, 6]", 4, new int[] { 1, 2, 3, 5, 6 })]
        [DataTestMethod]
        public void TestAddCenter(string expected, int item, int[] items)
        {
            Initialize(items);

            _mDeque.AddCenter(item);

            Assert.AreEqual(expected, _mDeque.ToString());
        }

        [DataRow("[0]", 0, new int[0])]
        [DataRow("[2, 3, 4]", 4, new int[] { 2, 3 })]
        [DataRow("[2, 4, 8, 16]", 16, new int[] { 2, 4, 8 })]
        [DataRow("[-4, -3, -2, -1]", -1, new int[] { -4, -3, -2 })]
        [DataRow("[4, 6, 8, 10]", 10, new int[] { 4, 6, 8 })]
        [DataTestMethod]
        public void TestAddLast(string expected, int item, int[] items)
        {
            Initialize(items);

            _mDeque.AddLast(item);

            Assert.AreEqual(expected, _mDeque.ToString());
        }

        [TestMethod]
        public void TestRemoveFirst_Empty()
        {
            Assert.ThrowsException<InvalidOperationException>(() => _mDeque.RemoveFirst());
        }

        [TestMethod]
        public void TestFirst_Empty()
        {
            Assert.ThrowsException<InvalidOperationException>(() => _mDeque.First);
        }

        [DataRow("[]", 0, new int[] { 0 })]
        [DataRow("[3, 4]", 2, new int[] { 2, 3, 4 })]
        [DataRow("[4, 8, 16]", 2, new int[] { 2, 4, 8, 16 })]
        [DataRow("[-3, -2, -1]", -4, new int[] { -4, -3, -2, -1 })]
        [DataRow("[6, 8, 10]", 4, new int[] { 4, 6, 8, 10 })]
        [DataTestMethod]
        public void TestRemoveFirst_NonEmpty(string expected, int item, int[] items)
        {
            Initialize(items);

            Assert.AreEqual(item, _mDeque.RemoveFirst());
            Assert.AreEqual(expected, _mDeque.ToString());
        }

        [TestMethod]
        public void TestRemoveCenter_Empty()
        {
            Assert.ThrowsException<InvalidOperationException>(() => _mDeque.RemoveCenter());
        }

        [TestMethod]
        public void TestCenter_Empty()
        {
            Assert.ThrowsException<InvalidOperationException>(() => _mDeque.Center);
        }

        [DataRow("[]", 1, new int[] { 1 })]
        [DataRow("[1]", 2, new int[] { 1, 2 })]
        [DataRow("[1, 3]", 2, new int[] { 1, 2, 3 })]
        [DataRow("[1, 2, 4, 5]", 3, new int[] { 1, 2, 3, 4, 5 })]
        [DataRow("[1, 2, 3, 5, 6]", 4, new int[] { 1, 2, 3, 4, 5, 6 })]
        [DataTestMethod]
        public void TestRemoveCenter_NonEmpty(string expected, int item, int[] items)
        {
            Initialize(items);

            Assert.AreEqual(item, _mDeque.RemoveCenter());
            Assert.AreEqual(expected, _mDeque.ToString());
        }

        [TestMethod]
        public void TestRemoveLast_Empty()
        {
            Assert.ThrowsException<InvalidOperationException>(() => _mDeque.RemoveLast());
        }

        [TestMethod]
        public void TestLast_Empty()
        {
            Assert.ThrowsException<InvalidOperationException>(() => _mDeque.Last);
        }

        [DataRow("[]", 0, new int[] { 0 })]
        [DataRow("[2, 3]", 4, new int[] { 2, 3, 4 })]
        [DataRow("[2, 4, 8]", 16, new int[] { 2, 4, 8, 16 })]
        [DataRow("[-4, -3, -2]", -1, new int[] { -4, -3, -2, -1 })]
        [DataRow("[4, 6, 8]", 10, new int[] { 4, 6, 8, 10 })]
        [DataTestMethod]
        public void TestRemoveLast_NonEmpty(string expected, int item, int[] items)
        {
            Initialize(items);

            Assert.AreEqual(item, _mDeque.RemoveLast());
            Assert.AreEqual(expected, _mDeque.ToString());
        }

        [TestMethod]
        public void TestCount_First()
        {
            int i = 0;

            do
            {
                Assert.AreEqual(_mDeque.Count, i);

                _mDeque.AddFirst(0);

                i++;
            }
            while (i < 4);

            while (i > 0)
            {
                Assert.AreEqual(_mDeque.Count, i);

                _mDeque.RemoveFirst();

                i--;
            }
        }

        [TestMethod]
        public void TestCount_Center()
        {
            int i = 0;

            do
            {
                Assert.AreEqual(_mDeque.Count, i);

                _mDeque.AddCenter(0);

                i++;
            }
            while (i < 4);

            while (i > 0)
            {
                Assert.AreEqual(_mDeque.Count, i);

                _mDeque.RemoveCenter();

                i--;
            }
        }

        [TestMethod]
        public void TestCount_Last()
        {
            int i = 0;

            do
            {
                Assert.AreEqual(_mDeque.Count, i);

                _mDeque.AddLast(0);

                i++;
            }
            while (i < 4);

            while (i > 0)
            {
                Assert.AreEqual(_mDeque.Count, i);

                _mDeque.RemoveCenter();

                i--;
            }
        }
    }
}
