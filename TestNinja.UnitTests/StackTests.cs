using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    internal class StackTests
    {
        [Test]
        public void Push_ElementIsNull_ArgumentNullException()
        {
            var stack = new Stack<object>();

            Assert.Throws<ArgumentNullException>(()=>stack.Push(null));
        }

        [Test]
        public void Push_AddObject_ReturnCountPlusOne()
        {
            var stack = new Stack<object>();

            stack.Push(1);

            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Pop_WhenCalled_RemoveLastElement()
        {
            var stack = new Stack<int>();
            stack.Push(1);

            stack.Pop();

            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_WhenCalled_ReturnLastElement()
        {
            var stack = new Stack<int>();
            stack.Push(1);

            var result = stack.Pop();

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Pop_CollectionIsEmpty_InvalidOperationException()
        {
            var stack = new Stack<object>();

            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }

        [Test]
        public void Peek_WhenCalled_RemoveLastElement()
        {
            var stack = new Stack<int>();
            stack.Push(1);

            stack.Peek();

            Assert.That(stack.Count, Is.EqualTo(1));
        }


        [Test]
        public void Peek_WhenCalled_ReturnLastElement()
        {
            var stack = new Stack<int>();
            stack.Push(1);

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Peek_CollectionIsEmpty_InvalidOperationException()
        {
            var stack = new Stack<object>();

            Assert.Throws<InvalidOperationException>(() => stack.Peek());
        }

        [Test]
        public void Count_WhenCalled_ReturnCount()
        {
            var stack = new Stack<object>();

            Assert.That(stack.Count, Is.EqualTo(0));
        }
    }
}
