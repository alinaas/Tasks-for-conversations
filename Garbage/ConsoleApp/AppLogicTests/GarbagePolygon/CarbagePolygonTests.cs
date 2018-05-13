using AppLogic.CarbagePoligon;
using AppLogic.Plant;
using AppLogic.Truck;
using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AppLogicTests.CarbagePoligon
{
    [TestFixture]
    public class CarbagePolygonTests 
    {
        private ITruck _truck;
        private IPlant _plant;
        private int _currentCapacity;
		private PlantType _availablePlant;
		private List<DriveInfo> _driveInfos;

		[SetUp]
        public void SetUp()
        {
            _truck = A.Fake<ITruck>();
            _plant = A.Fake<IPlant>();

            _currentCapacity = 0;
			_availablePlant = PlantType.Reutov;
			_driveInfos = new List<DriveInfo>();
            A.CallTo(() => _truck.GetCapacity()).ReturnsLazily(() => _currentCapacity);
			A.CallTo(() => _truck.GetAvaliablePlant()).ReturnsLazily(() => _availablePlant);
			A.CallTo(() => _truck.DriveToPlant(A<DriveInfo>.Ignored)).
				Invokes((DriveInfo driveInfo) =>
					_driveInfos.Add(driveInfo));
        }


        [TestCase (5)]
        public void WeightMoreThanCapacity_NothingHappened(int weight)
        {
            _currentCapacity = 1;
            var result = CreateCarbagePolygon().DisposeGarbage(weight);

            A.CallTo(() => _truck.GetCapacity()).MustHaveHappenedOnceExactly();
            Assert.That(result.IsSuccess, Is.False);
            A.CallTo(() => _truck.LoadGarbage(weight)).MustNotHaveHappened();
            A.CallTo(_plant).MustNotHaveHappened();
        }

		[TestCase(1)]
		public void PlantIsNotRublevka_NothingHappened(int weight)
		{
			_currentCapacity = 5;
			var result = CreateCarbagePolygon().DisposeGarbage(weight);
			A.CallTo(() => _truck.GetCapacity()).MustHaveHappenedOnceExactly().
				Then(A.CallTo(() => _truck.LoadGarbage(weight)).MustHaveHappenedOnceExactly()).
				Then(A.CallTo(() => _truck.GetAvaliablePlant()).MustHaveHappenedOnceExactly());
			Assert.That(result.IsSuccess, Is.False);
			A.CallTo(_plant).MustNotHaveHappened();
			//надо ли остальные методы _truck проверять

		}

        private CarbagePolygon CreateCarbagePolygon()
        {
            return new CarbagePolygon(_truck, _plant);
        }
    }
}
