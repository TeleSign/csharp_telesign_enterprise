.. image:: https://raw.github.com/TeleSign/csharp_telesign/master/csharp_banner_enterprise.jpg
    :target: https://developer.telesign.com

==========================
TeleSign Enterprise C# SDK
==========================

TeleSign is a communications platform as a service (CPaaS) company, founded on security. Since 2005, TeleSign has
been a trusted partner to the world’s leading websites and mobile applications, helping secure billions of end-user
accounts. Today, TeleSign’s data-driven, cloud communications platform is changing the way businesses engage with
customers and prevent fraud.

For more information about TeleSign, visit our `website <http://www.TeleSign.com>`_.

Documentation
-------------

Code documentation is included in the SDK. Complete documentation, quick start guides and reference material
for the TeleSign API is available within the `TeleSign Developer Center <https://developer.telesign.com/>`_.

Installation
------------

To add the TeleSign Enterprise C# SDK to your project, build and install the TelesignEnterprise package:

.. code-block:: bash

    $ nuget pack .\Path\To\csharp_telesign_enterprise\TelesignEnterprise\TelesignEnterprise.csproj
    $ Install-Package .\Path\To\TelesignEnterprise-(insert latest version).nupkg

Authentication
--------------

You will need a Customer ID and API Key in order to use TeleSign’s REST API. If you are already a customer and need an
API Key, you can retrieve it from `TelePortal <https://teleportal.telesign.com>`_.

Dependencies
------------

We make use of popular, feature-rich and well-tested open-source libraries to perform the underlying functionality of
the SDK. These dependencies are managed by the community accepted package manager. If you are unable to add these
additional third party dependencies to your project we have ensured that the SDK code is easy to read and can serve as
sample code. We have also made sure that more complicated functions such as GenerateTelesignHeaders can be easily
extracted from the SDK and used 'as is' in your project.

C# Code Example: Verify SMS
---------------------------

Here is a basic code example with JSON response.

.. code-block:: java

    string customerId = "FFFFFFFF-EEEE-DDDD-1234-AB1234567890";
    string apiKey = "EXAMPLETE8sTgg45yusumoN6BYsBVkh+yRJ5czgsnCehZaOYldPJdmFh6NeX8kunZ2zU1YWaUw/0wV6xfw==";

    string phoneNumber = "phone_number";
    
    VerifyClient verifyClient = new VerifyClient(customerId, apiKey);
    RestClient.TelesignResponse telesignResponse = verifyClient.sms(phoneNumber);

.. code-block:: javascript
    
    {'errors': [],
     'reference_id': '25685A40218006049044A58789044948',
     'resource_uri': '/v1/verify/25685A40218006049044A58789044948',
     'status': {'code': 290,
        'description': 'Message in progress',
        'updated_on': '2017-02-07T03:13:42.610863Z'},
     'sub_resource': 'sms',
     'verify': {'code_entered': '',
        'code_state': 'UNKNOWN'}}

For more examples, see the
`examples <https://github.com/TeleSign/csharp_telesign_enterprise/tree/master/TelesignEnterprise.Example>`_ project or visit
the `TeleSign Developer Portal <https://developer.telesign.com/>`_.
