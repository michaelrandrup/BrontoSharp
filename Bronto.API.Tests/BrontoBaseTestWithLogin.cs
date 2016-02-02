using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bronto.API.Tests
{
    public abstract class BrontoBaseTestWithLogin : BrontoBaseTestClass
    {
        LoginSession _login = null;

        public LoginSession Login
        {
            get { return _login ?? (_login = Bronto.API.LoginSession.Create(ApiToken)); }
            set { _login = value; }
        }
    }
}
