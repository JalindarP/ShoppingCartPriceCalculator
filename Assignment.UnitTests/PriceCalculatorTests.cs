using System;
using Xunit;
using Assignment;
using Assignement.CartBL.Interface;
using Assignement.CartBL;
using System.Collections.Generic;
using Shouldly;
using Assignement.Model;

namespace Assignment.UnitTests
{
    public class PriceCalculatorTests
    {

        private IPriceCalculator _target;

        public PriceCalculatorTests()
        {
            _target = new PriceCalculator();
        }

        [Fact]
        public void ReadOffers()
        {
        }

        [Fact]
        public void ReadShoppingItems()
        { }

        [Fact]
        public void AddFreeItems() { }

        [Fact]
        public void AddInCart() { }

    }
}
