using AwesomeLib;
using AwesomeLib.Cellphone;
using AwesomeLib.Eyes;
using AwesomeLib.Legs;
using AwesomeLib.Weapon;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwesomeLibTests
{
    [TestFixture]
    public class BotTests
    {
        private IWeapon _weapon;
        private ILegs _legs;
        private IEyes _eyes;
        private ICellphone _cellphone;
        private int _mobLocation;
        private bool _checkMobStatusResult;
        public static int[] Locations = Enumerable.Range(1, 5).ToArray();
        private List<Report> _reports;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }

        [SetUp]
        public void SetUp()
        {
            _weapon = A.Fake<IWeapon>();
            _legs = A.Fake<ILegs>();
            _eyes = A.Fake<IEyes>();
            _cellphone = A.Fake<ICellphone>();
            _checkMobStatusResult = true;

            _mobLocation = 0;
            _reports = new List<Report>();
            A.CallTo(() => _eyes.CheckForMob()).ReturnsLazily(() => _mobLocation);
            A.CallTo(() => _weapon.Strike()).Returns(1);
            A.CallTo(() => _eyes.CheckMobStatus()).ReturnsLazily(() => _checkMobStatusResult);
            A.CallTo(() => _cellphone.ReportResult(A<Report>.Ignored))
                .Invokes((Report report) =>
                {
                    _reports.Add(report);
                });
        }

        [Test]
        public void MobWasNotFound_NothingHappened()
        {
            var result = CreateBot().KillMob();

            Assert.That(result, Is.False);
            A.CallTo(_legs).MustNotHaveHappened();
            A.CallTo(_weapon).MustNotHaveHappened();
        }

		[Test]
		public void MobWasNotFound_NothingHappend_Copy()
		{
			var bot = CreateBot();
			var result = bot.KillMob();
			Assert.That(result, Is.False);
			A.CallTo(() => _legs.MoveTo(_mobLocation)).MustNotHaveHappened();
			A.CallTo(() => _cellphone.ReportResult(A<Report>.Ignored)).MustNotHaveHappened();
			A.CallTo(() => _eyes.CheckMobStatus()).MustNotHaveHappened();
		}

		[Test, TestCaseSource(nameof(Locations))]
        public void MobWasFound_MobWasStriked(int mobLocation)
        {
            _mobLocation = mobLocation;

            var result = CreateBot().KillMob();

            Assert.That(result, Is.True);

            A.CallTo(() => _eyes.CheckForMob()).MustHaveHappenedOnceExactly()
                .Then(A.CallTo(() => _legs.MoveTo(mobLocation)).MustHaveHappenedOnceExactly())
                .Then(A.CallTo(() => _weapon.Strike()).MustHaveHappened(Repeated.Exactly.Times(3)))
                .Then(A.CallTo(() => _cellphone.ReportDamage(A<int[]>.That.IsSameSequenceAs(new[] { 1, 1, 1 }))).MustHaveHappenedOnceExactly())
                .Then(A.CallTo(() => _eyes.CheckMobStatus()).MustHaveHappenedOnceExactly())
                .Then(A.CallTo(() => _cellphone.ReportResult(A<Report>.Ignored)).MustHaveHappenedOnceExactly());

            Assert.That(_reports.Count, Is.EqualTo(1));
            Assert.That(_reports[0].IsSuccess, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void KillMob_Reports_CorrecStatus(bool status)
        {
            _checkMobStatusResult = status;
            _mobLocation = 10;

            var result = CreateBot().KillMob();

            Assert.That(_reports.Count, Is.EqualTo(1));
            Assert.That(_reports[0].IsSuccess, Is.EqualTo(status));
        }

        [Test]
        public void KillMob_ThrowsException_If_CheckMobStatusDoes()
        {
            _mobLocation = 10;
            A.CallTo(() => _eyes.CheckMobStatus()).Throws(new NotSupportedException());

            Assert.Throws(typeof(NotSupportedException), 
                () => CreateBot().KillMob());
        }

        private Bot CreateBot()
        {
            return new Bot(_weapon, _legs, _eyes, _cellphone);
        }
    }
}
