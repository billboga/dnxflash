﻿using Xunit;

namespace MarqueeMessenger.Tests
{
    public class InMemoryStackMessengerTests
    {
        public InMemoryStackMessengerTests()
        {
            sut = new InMemoryStackMessenger();
        }

        private readonly InMemoryStackMessenger sut;

        [Fact]
        public void Should_add_new_items_on_top_of_previous_item_and_fetch_from_top()
        {
            sut
                .Add(new MarqueeMessage("first"))
                .Add(new MarqueeMessage("second"));

            var expectedTop = sut.Fetch();
            var expectedBottom = sut.Fetch();

            Assert.NotNull(expectedTop);
            Assert.NotNull(expectedBottom);

            Assert.Equal("second", expectedTop.Message);
            Assert.Equal("first", expectedBottom.Message);
        }
    }
}