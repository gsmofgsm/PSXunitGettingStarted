using System;
using Xunit;

namespace GameEngine.Tests
{
    public class PlayerCharacterShould
    {
        [Fact]
        public void BeInexperiencedWhenNew()
        {
            PlayerCharacter sut = new PlayerCharacter();
            Assert.True(sut.IsNoob);
        }

        [Fact]
        public void CalculateFullName()
        {
            PlayerCharacter sut = new PlayerCharacter();

            sut.FirstName = "Sarah";
            sut.LastName = "Smith";

            Assert.Equal("Sarah Smith", sut.FullName);
            Assert.StartsWith("Sarah", sut.FullName);
            Assert.EndsWith("Smith", sut.FullName);
            Assert.Contains("rah Smi", sut.FullName);
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", sut.FullName);
        }


        [Fact]
        public void StartWithDefaultHealth()
        {
            PlayerCharacter sut = new PlayerCharacter();

            Assert.Equal(100, sut.Health);
        }


        [Fact]
        public void StartWithDefaultHealth_NotEqualExample()
        {
            PlayerCharacter sut = new PlayerCharacter();

            Assert.NotEqual(0, sut.Health);
        }


        [Fact]
        public void IncreaseHealthAfterSleeping()
        {
            PlayerCharacter sut = new PlayerCharacter();

            sut.Sleep();

            Assert.InRange<int>(sut.Health, 101, 200);
        }

        [Fact]
        public void NotHaveNickNameByDefault()
        {
            PlayerCharacter sut = new PlayerCharacter();
            Assert.Null(sut.Nickname);
        }

        [Fact]
        public void HaveALongBow()
        {
            PlayerCharacter sut = new PlayerCharacter();
            Assert.Contains("Long Bow", sut.Weapons);
        }

        [Fact]
        public void NotHaveAStaffOfWonder()
        {
            PlayerCharacter sut = new PlayerCharacter();
            Assert.DoesNotContain("Staff Of Wonder", sut.Weapons);
        }

        [Fact]
        public void HaveAtLeastOneKindOfSword()
        {
            PlayerCharacter sut = new PlayerCharacter();
            Assert.Contains(sut.Weapons, weapon => weapon.Contains("Sword"));
        }

        [Fact]
        public void HaveAllExpectedWeapons()
        {
            PlayerCharacter sut = new PlayerCharacter();

            var expectedWeapons = new[]
            {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };

            Assert.Equal(expectedWeapons, sut.Weapons);
        }

        [Fact]
        public void HaveNoEmptyDefaultWeapons()
        {
            PlayerCharacter sut = new PlayerCharacter();
            Assert.All(sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
        }

        [Fact]
        public void RaisedSleptEvent()
        {
            PlayerCharacter sut = new PlayerCharacter();

            Assert.Raises<EventArgs>(
                handler => sut.PlayerSlept += handler,
                handler => sut.PlayerSlept -= handler,
                () => sut.Sleep());
        }

        [Fact]
        public void RaisedPropertyChangedEvent()
        {
            PlayerCharacter sut = new PlayerCharacter();
            Assert.PropertyChanged(sut, "Health", () => sut.TakeDamage(10));
        }
    }
}
