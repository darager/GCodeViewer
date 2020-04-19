using System;
using FluentAssertions;
using NUnit.Framework;

namespace GCodeViewer.WPF.MVVM.Helpers.Tests
{
    public class ActionHelpersTests
    {
        [Test]
        public void ThrottleActionWorks()
        {
            int count = 0;
            Action action = () => count++;

            for (int i = 0; i < 10; i++)
            {
                action.Throttle(5);
            }

            count.Should().Be(1);
        }
    }
}