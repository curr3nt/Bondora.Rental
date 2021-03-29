**Bondora.Rental** is an implementation of Bondora .NET developer [home assigment](https://docs.google.com/document/d/1Xcvgj6U8pY7OaDbBYju1Uw4wGTWmf8P-6R3nWqw18ZQ/edit#).
Solution consists of 4 projects:
* `Domain` contains primary business logic
* `Domain.Interface` exposes business logic as *NServiceBus* endpoint
* `Tests` contains unit tests for business logic
* `Web` describes web user interface and interacts with `Domain.Interface` endpoint

To run solution, please use Visual Studio. Set both `Domain.Interface` and `Web` as startup projects, and hit `F5`.

To run unit tests, please use *Test Explorer* view in Visual Studio.

Since this is my first time working with NServiceBus, I am not enterily confident in the end result. In case solution will fail to generate invoice, please refer to the [latest version of code before NServiceBus was added](https://github.com/curr3nt/Bondora.Rental/tree/before-NServiceBus).