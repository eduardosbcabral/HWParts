using HWParts.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HWParts.Tests
{
    public class AccountCommandTests
    {
        [Fact]
        public void Command_without_username_is_invalid()
        {
            var fakeCommand = new RegisterAccountCommand(
                "",
                "test_user@test.com",
                "123456");

            fakeCommand.Validate();

            Assert.True(fakeCommand.Invalid);
            Assert.Equal(1, fakeCommand.Notifications.Count);
        }

        [Fact]
        public void Command_without_email_is_invalid()
        {
            var fakeCommand = new RegisterAccountCommand(
                "testUser",
                "",
                "123456");

            fakeCommand.Validate();

            Assert.True(fakeCommand.Invalid);
            Assert.Equal(1, fakeCommand.Notifications.Count);
        }

        [Fact]
        public void Command_without_password_is_invalid()
        {
            var fakeCommand = new RegisterAccountCommand(
                "testUser",
                "test_user@test.com",
                "");

            fakeCommand.Validate();

            Assert.True(fakeCommand.Invalid);
            Assert.Equal(1, fakeCommand.Notifications.Count);
        }

        [Fact]
        public void Command_is_valid()
        {
            var fakeCommand = new RegisterAccountCommand(
                "testUser",
                "test_user@test.com",
                "123456");

            fakeCommand.Validate();

            Assert.True(fakeCommand.Valid);
            Assert.Equal(0, fakeCommand.Notifications.Count);
        }
    }
}
