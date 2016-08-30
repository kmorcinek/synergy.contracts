using System;
using NUnit.Framework;
using Synergy.Contracts.Samples;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailDateTimeTest
    {
        [Test]
        public void IfNotMidnight()
        {
            DateTime now = DateTime.Today.AddSeconds(1000);

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotMidnight( now, "ojtam ojtam")
                );

            Fail.IfNotMidnight( DateTime.Today, "ojtam ojtam");
            Fail.IfNotMidnight(null, "ojtam ojtam");
        }

        [Test]
        public void IfNotMidnightSample()
        {
            // ARRANGE
            IContractorRepository contractorRepository = new ContractorRepository();
            var ratherNotMidnight = DateTime.Now;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => contractorRepository.GetContractorsAged(minDate: ratherNotMidnight, maxDate: null));

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("minDate must be a midnight"));
        }
    }
}