using System;
using NUnit.Framework;
using TelesignEnterprise;

namespace TelesignEnterprise.Test
{
    [TestFixture]
    [Category("Unit")]
    public class VoiceClientTest
    {
        [Test]
        public void TestExposesDependencyMethods()
        {
            DependencyCheckHelper.VerifyDependencyMethods(
                typeof(VoiceClient),
                typeof(Telesign.VoiceClient),
                "Call",
                "CallAsync",
                "Status",
                "StatusAsync"
            );
        }
    }
}
