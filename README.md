# Sitecore.Foundation.SitecoreSend

## Introduction

This is a wrapper to access the Sitecore Send API. It builds on the [Moosend C# Wrapper](https://github.com/moosend/api-wrappers-dotnet).

## Setup

### Infrastructure

Make sure the environment variable `SITECORE_SEND_API_KEY` is available in the system this code is being executed.

### Dependeny injection

If you want to use dependency injection, then make sure that Foundation.SitecoreSend.config has been deployed to your system.

## Usage

### Add a subsriber to an email list

This method either adds a new subscriber or updates an existing subscriber.

```cs
var emailListId = "xyz";
var email = "john.doe@gmail.com";
var name = "John Doe";
var customFields = new Dictionary<string, string>
{
	{ "FirstName", "John" },
	{ "LastName", "Doe" }
};
var isMarketingConsent = false;

var isSuccess = _sitecoreSendService.AddSubscriber(emailListId, email, name, customFields, isMarketingConsent);
```

### Add multiple subsribers to an email list

This method either adds a new subscribers or updates existing subscribers.

```cs
var emailListId = "xyz";
var multipleSubscribers = new List<Subscribers>
{
	new Subscribers(
		"john.doe+1@gmail.com", 
		"John Doe 1", 
		_sitecoreSendService.ConvertCustomFields(new Dictionary<string, string>
			{
				{ "FirstName", "John" },
				{ "LastName", "Doe 1" }
			})),
	new Subscribers(
		"john.doe+2@gmail.com", 
		"John Doe 2", 
		_sitecoreSendService.ConvertCustomFields(new Dictionary<string, string>
			{
				{ "FirstName", "John" },
				{ "LastName", "Doe 2" }
			}))
};

var isSuccess = _sitecoreSendService.AddMultipleSubscribers(emailListId, subscribers);
```

### Get all subscribers of an email list

```cs
var emailListId = "xyz";

var subscribers = _sitecoreSendService.GetSubscribers(emailListId, Sitecore.Foundation.SitecoreSend.Model.SubscriberStatus.Subscribed);
```