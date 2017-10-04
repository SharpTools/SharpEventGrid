# SharpEventGrid

Simple library to send Events to Azure EventGrid


## Nuget

```
Install-Package sharpeventgrid
```

From Microsoft:

>Azure Event Grid is an innovative offering that makes an event a first-class object in Azure. With Azure Event Grid, you can subscribe to any event that is happening across your Azure resources and react using serverless platforms like Functions or Logic Apps. In addition to having built-in publishing support for events with services like Blob Storage and Resource Groups, Event Grid provides flexibility and allows you to create your own custom events to publish directly to the service.

This library simplify the sending of custom events to Azure EventGrid from your .net application.

## How to use

Create your client:
```cs
var client = new EventGridClient(new Uri(eventGridUrl), eventGridKey);
```

Create the event to send
```cs
var newEvent = new Event {
    Subject = "customer-service/customers",
    Topic = "your topic here",
    EventType = "customer-new",
    Data = new Customer { Name = "Joe" }
};
```

Send it!
```cs
await client.Send(newEvent);
```

