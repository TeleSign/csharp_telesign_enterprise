2.4.0
- Added supported version of .NET Core 6, 7, 8, and 9
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