using System;
using NUnit.Framework;
using TelesignEnterprise;

namespace TelesignEnterprise.Test
{
    [TestFixture]
    [Category("Unit")]
    public class PhoneIdClientTest
    {
        [Test]
        public void TestExposesDependencyMethods()
        {
            DependencyCheckHelper.VerifyDependencyMethods(
                typeof(PhoneIdClient),
                typeof(Telesign.PhoneIdClient),
                "PhoneIdPath",
                "PhoneIdPathAsync"
            );
        }
    }
}
