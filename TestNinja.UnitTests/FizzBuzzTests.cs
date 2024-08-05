using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;


namespace TestNinja.UnitTests
{
    [TestFixture]
    internal class FizzBuzzTests
    {
        [Test]
        public void FizzBuzz_DevidableOnlyByThree_ReturnFizz()
        {
            var fizzbuzz = new FizzBuzz();

            var result = fizzbuzz.GetOutput(3);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void FizzBuzz_DevidableOnlyByFive_ReturnBuzz()
        {
            var fizzbuzz = new FizzBuzz();

            var result = fizzbuzz.GetOutput(5);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void FizzBuzz_DevidableByThreeAndFive_ReturnFizzBuzz()
        {
            var fizzbuzz = new FizzBuzz();

            var result = fizzbuzz.GetOutput(15);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void FizzBuzz_NotDevidableByThreeAndFive_ReturnFizzBuzz()
        {
            var fizzbuzz = new FizzBuzz();

            var result = fizzbuzz.GetOutput(1);

            Assert.That(result, Is.EqualTo("1"));
        }

    }
}
