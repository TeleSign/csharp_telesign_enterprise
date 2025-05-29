2.6.1
- Refactored C# SDK CreateVerificationProcess method in VerifyClient to use the new method of the same name in OmniVerifyClient class.
  This method in VerifyClient is now deprecated.
- Added the string parameter called “phoneNumber” to OmniVerifyClient class
- Updated version in the TelesignEnterprise.csproj file 

2.6.0
- Added C# SDK method for Retrieve a Verification Process action
- Added the TelesignEnterprise.Test folder for adding the unit and integrated test cases

2.5.0
- Added tracking to requests
- Updated instructions in README file to install this SDK by default using the latest version.

2.4.0
- Added supported versions of .NET Core 6, 7, 8, and 9
- Updated syntax code to support .NET framework and .Net core

2.3.0
- Added new method to send requests to Telesign Messaging.
- Added new method to send requests to the Telesign Verify API.
- Added new new examples for requests to Telesign Omnichannel Messaging.
- Added new examples for requests to the Telesign Verify API.
- Removed push method in VerifyClient class.

2.2.0
 - AutoVerify rebranded to AppVerify, please update your integration

2.1.0
- Major refactor and simplification into generic REST client.
- updated and improved README
- added travis CI, codecov coverage and additional unit tests
- API parameters are now passed as a dictionary to endpoint handlers.
- UserAgent is now set to track client usage and help debug issues.
- GenerateTelesignHeaders is now static and easily extracted from the SDK if
  custom behavior/implementation is required.

1.0.0
- Initial release