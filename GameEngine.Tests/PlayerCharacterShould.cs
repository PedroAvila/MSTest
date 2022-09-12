using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameEngine.Tests
{
    [TestClass]
    public class PlayerCharacterShould
    {
        PlayerCharacter sut;

        [TestInitialize]
        public void TestInitialize()
        {
            sut = new PlayerCharacter();
            sut.FirstName = "Sarah";
            sut.LastName = "Smith";
        }

        [TestMethod]
        //[TestCategory("Player Defaults")]
        [PlayerDefaults]
        //[Ignore]
        public void BeInexperiencedWhenNew()
        {
            Assert.IsTrue(sut.IsNoob);
        }

        [TestMethod]
        //[TestCategory("Player Defaults")]
        [PlayerDefaults]
        //[Ignore("Temporarily disabled for refactoring")]
        public void NotHaveNickNameByDefault()
        {
            //var sut = new PlayerCharacter();

            Assert.IsNull(sut.Nickname);
        }

        [TestMethod]
        //[TestCategory("Player Defaults")]
        [PlayerDefaults]
        public void StartWithDefaultHealth()
        {
            Assert.AreEqual(100, sut.Health);
        }

        public static IEnumerable<object[]> Damages
        {
            get
            {
                return new List<object[]>
                {
                    new object[]{1,99},
                    new object[]{0,100},
                    new object[]{100,1},
                    new object[]{101,1},
                    new object[]{50,50},
                    new object[]{40,60}
                };
            }
        }

        public static IEnumerable<object[]> GetDamages()
        {
            return new List<object[]>
                {
                    new object[]{1,99},
                    new object[]{0,100},
                    new object[] { 100, 1 },
                    new object[] { 101, 1 },
                    new object[] { 50, 50 },
                    new object[] { 10, 90 }
                };
        }

        [DataTestMethod]
        //[DynamicData(nameof(Damages))]
        //[DynamicData(nameof(GetDamages), DynamicDataSourceType.Method)]
        //[DynamicData(nameof(DamageData.GetDamages), typeof(DamageData), DynamicDataSourceType.Method)]
        //[DynamicData(nameof(ExternalHealthDamageTestData.TestData), typeof(ExternalHealthDamageTestData))]
        [CsvDataSource("Damage.csv")]
        //[DataRow(1, 99)]
        //[DataRow(0, 100)]
        //[DataRow(100, 1)]
        //[DataRow(101, 1)]
        //[DataRow(50, 50)]
        //[TestCategory("Player Health")]
        [PlayerHealth]
        public void TakeDamage(int damage, int expectedHealth)
        {
            sut.TakeDamage(damage);

            Assert.AreEqual(expectedHealth, sut.Health);
        }

        [TestMethod]
        //[TestCategory("Player Health")]
        [PlayerHealth]
        public void TakeDamage_NotEqual()
        {
            sut.TakeDamage(1);
            Assert.AreNotEqual(100, sut.Health);
        }

        [TestMethod]
        [PlayerHealth]
        [TestCategory("Another Category")]
        public void IncreaseHealthAfterSleeping()
        {
            sut.Sleep(); // Se espera un aumento entre 1 y 100 inclusive

            //Assert.IsTrue(sut.Health >= 101 && sut.Health <= 200);
            Assert.That.IsInRange(sut.Health, 101, 200);
        }

        [TestMethod]
        public void CalculateFullName()
        {
            //sut.FirstName = "Sarah";
            //sut.LastName = "Smith";

            Assert.AreEqual("SARAH SMITH", sut.FullName, true);
        }

        [TestMethod]
        public void HaveFullNameStartingWithFirstName()
        {
            //sut.FirstName = "Sarah";
            //sut.LastName = "Smith";

            //Assert.IsTrue(sut.FullName.StartsWith("Sarah"));

            StringAssert.StartsWith(sut.FullName, "Sarah");
        }

        [TestMethod]
        public void HaveFullNameEndingWithLastName()
        {
            sut.LastName = "Smith";
            StringAssert.EndsWith(sut.FullName, "Smith");
        }

        [TestMethod]
        public void CalculateFullName_SubstringAssertExample()
        {
            //sut.FirstName = "Sarah";
            //sut.LastName = "Smith";

            StringAssert.Contains(sut.FullName, "ah Sm");
        }

        [TestMethod]
        public void CalculateFullNameWithTitleCase()
        {
            //sut.FirstName = "Sarah";
            //sut.LastName = "Smith";

            StringAssert.Matches(sut.FullName, new Regex("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
            //StringAssert.DoesNotMatch(sut.FullName, new Regex("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
        }

        [TestMethod]
        public void HaveALongBow()
        {
            CollectionAssert.Contains(sut.Weapons, "Long Bow");
        }

        [TestMethod]
        public void NotHaveAStaffOfWonder()
        {
            CollectionAssert.DoesNotContain(sut.Weapons, "Staff Of Wonder");
        }

        [TestMethod]
        public void HaveAllExpectedWeapons()
        {
            var expectedWeapons = new[]
            {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };

            CollectionAssert.AreEqual(expectedWeapons, sut.Weapons);
        }

        [TestMethod]
        public void HaveAllExpectedWeapons_AnyOrder()
        {
            var expectedWeapons = new[]
            {
                "Short Bow",
                "Long Bow",
                "Short Sword"
            };

            CollectionAssert.AreEquivalent(expectedWeapons, sut.Weapons);
        }

        [TestMethod]
        public void HaveNoDuplicateWeapons()
        {
            //sut.Weapons.Add("Short Bow");

            CollectionAssert.AllItemsAreUnique(sut.Weapons);
        }

        [TestMethod]
        public void HaveAtLeastOneKindOfSword()
        {
            //Assert.IsTrue(sut.Weapons.Any(weapon => weapon.Contains("Sword")));
            CollectionAssert.That.AtLeastOneItemSatisfies(sut.Weapons,
                                                            weapon => weapon.Contains("Sword"));
        }

        [TestMethod]
        public void HaveNoEmptyDefaultWeapons()
        {
            //Assert.IsFalse(sut.Weapons.Any(weapon => string.IsNullOrWhiteSpace(weapon)));
            //CollectionAssert.That.AllItemsNotNullOrWhitespace(sut.Weapons);

            //sut.Weapons.Add(" ");
            //CollectionAssert.That.AllItemsSatisfy(sut.Weapons,
            //                                        weapon => !string.IsNullOrWhiteSpace(weapon));

            CollectionAssert.That.All(sut.Weapons, weapon =>
            {
                StringAssert.That.NotNullOrWhiteSpace(weapon);
                Assert.IsTrue(weapon.Length > 5);
            });
        }
    }
}
