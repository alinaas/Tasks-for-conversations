using AwesomeLib;
using AwesomeLib.Cellphone;
using AwesomeLib.Eyes;
using AwesomeLib.Legs;
using AwesomeLib.Weapon;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AwesomeLibTestsXUnit
{
	public class BotXUnitTests
	{
		private Mock<IWeapon> _weapon;
		private Mock<ILegs> _legs;
		private Mock<IEyes> _eyes;
		private Mock<ICellphone> _cellphone;
		private int _mobLocation;
		private List<Report> _reports;
		private bool _checkMobStatus;

		//*********
		public static IEnumerable<object[]> MobLocations =
		new List<object[]>
		{
			new object[] { 1},
			new object[] { 2},
			new object[] { 3}
		};



		//Enumerable.Range(1, 5).ToArray();

		public BotXUnitTests()
		{
			_weapon = new Mock<IWeapon>(MockBehavior.Strict);
			_legs = new Mock<ILegs>();
			_eyes = new Mock<IEyes>(MockBehavior.Strict);
			_cellphone = new Mock<ICellphone>();
			_mobLocation = 0;
			_checkMobStatus = true;
			_reports = new List<Report>();
			_eyes.Setup(ss => ss.CheckForMob()).Returns(() => _mobLocation);
			_weapon.Setup(ss => ss.Strike()).Returns(1);
			_legs.Setup(ss => ss.MoveTo(_mobLocation));
			_eyes.Setup(ss => ss.CheckMobStatus()).Returns(()=>_checkMobStatus);
			_cellphone.Setup(cs => cs.ReportResult(It.IsAny<Report>()))
			 .Callback<Report>(
				(report) =>
				{
					_reports.Add(report);
				});
		}

		[Fact]
		public void MobWasNotFound_NothingHappened()
		{
			_mobLocation = 0;
			//_eyes.Setup(ss => ss.CheckForMob()).Returns(_mobLocation);
			var result = CreateBot().KillMob();
			Assert.False(result);
			//_legs.Verify(x => x.MoveTo(It.IsAny<int>()), Times.Never);
			//_weapon.Verify(x => x.Strike(), Times.Never);
			_legs.Verify();
			_weapon.Verify();
		}

		[Theory]
		//[InlineData(1)]
		[MemberData(nameof(MobLocations))]
		public void MobWasFound_MobWasStriked(int mobLocation)
		{
			_mobLocation = mobLocation;
			int[] expectedArray = new int[] { 1, 1, 1 };
			//_eyes.Setup(ss => ss.CheckForMob()).Returns(_mobLocation);
			
			
			
			
			var result = CreateBot().KillMob();
			Assert.True(result);
			_eyes.Verify(x => x.CheckForMob(), Times.Once);
			_legs.Verify(x => x.MoveTo(_mobLocation), Times.Once);
			_weapon.Verify(x => x.Strike(), Times.Exactly(3));
			//**********
			_cellphone.Verify(x => x.ReportDamage(It.Is<int[]>(a => a.Where((b, i) => b == expectedArray[i]).Count() == expectedArray.Length)), Times.Once);
			_eyes.Verify(x => x.CheckMobStatus(), Times.Once);

			//**************
			_cellphone.Verify(x => x.ReportResult(It.Is<Report>(a => a.IsSuccess == true)), Times.Once);

			Assert.Single(_reports);
			Assert.True(_reports[0].IsSuccess);



			/*var moq = new Mock<IMyService>();
moq.Setup(m => m.AuthoriseUser(It.IsAny<AuthDetails>(),
                                It.IsAny<Action<AuthResult>>(),
                                It.IsAny<Action<AuthResult>>()))
    .Callback<AuthDetails, Action<AuthResult>, Action<AuthResult>>(
    (loginDetails, onSuccess, onFailure) =>
        {
            onSuccess(new AuthResult()); // fire onSuccess
            onFailure(new AuthResult()); // fire onFailure
        });*/


		}

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public void KillMob_Reports_CorrectStatus(bool status)
		{
			_mobLocation = 10;
			_checkMobStatus = status;
			var result = CreateBot().KillMob();
			Assert.Single(_reports);
			Assert.Equal(_reports[0].IsSuccess, _checkMobStatus);
		}


		[Fact]
		public void KillMob_ThrowsException_If_CheckMobStatusDoes()
		{
			_mobLocation = 10;
			_eyes.Setup(ey => ey.CheckMobStatus()).Throws(new NotSupportedException());
			
			Assert.Throws<NotSupportedException>(() => CreateBot().KillMob());
			
		}

		private Bot CreateBot()
		{
			return new Bot(_weapon.Object, _legs.Object, _eyes.Object, _cellphone.Object);
		}
	}
}
