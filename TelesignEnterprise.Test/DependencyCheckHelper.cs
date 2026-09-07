using System;
using System.Reflection;
using NUnit.Framework;

namespace TelesignEnterprise.Test
{
    public static class DependencyCheckHelper
    {
        public static void VerifyDependencyMethods(Type enterpriseClientType, Type dependencyClientType, params string[] methodNames)
        {
            Assert.That(
                enterpriseClientType.IsSubclassOf(dependencyClientType),
                Is.True,
                $"{enterpriseClientType.Name} should inherit from {dependencyClientType.Name}"
            );

            // Verify each method exists and is public
            foreach (var methodName in methodNames)
            {
                var method = enterpriseClientType.GetMethod(
                    methodName,
                    BindingFlags.Public | BindingFlags.Instance
                );

                Assert.That(
                    method,
                    Is.Not.Null,
                    $"Method '{methodName}' should exist in {enterpriseClientType.Name}"
                );

                Assert.That(
                    method.IsPublic,
                    Is.True,
                    $"Method '{methodName}' should be public in {enterpriseClientType.Name}"
                );
            }
        }
    }
}
