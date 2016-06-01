# Mini Billing Server

The leaked server files require an IIS server and some ASP pages to access the silk-table of the Database. The IIS is a huge bloatware for this small task. This small programm emulates a billing server.

This program is aimed for primary for developers and not for productive usage.

## Setup

* Enter your database credentials into `SilkDB.cs`
* Compile
* and run
* Set your billing server url to `http://localhost:8080/`


## Extend

It works for basic silk transactions. I don't know which features are required to make it a full featured billing server.

Each URL should be handled in its own Handler, derived from `IHttpHandler`. Try to keep the code clean by placing database-related stuff into the `SilkDB.cs`.

Whats missing is an Host/IP-based filter to prevent everyone from accessing the server.


## License

Free for everyone to do anything. Change or reuse what ever you like. You may even sell it. But that would not be fair for the buyer ...


