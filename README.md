# ProductFrontend

This program loads data from a database, sorts and optimizes the data and presents it in a MVC application.

## Installation

1. Download the database saving application at: https://github.com/vino00001/SaveToDB and follow the instructions.
2. Insert your connection string in Web.config (same connection string as in the database app).

```
  <add name="csvDB" connectionString="*INSERT CONNECTION STRING HERE*"
  providerName="System.data.MySqlClient" />
```

3. Run the program.
4. Navigate to ~/home/sku/**sku-id** to show optimized data for the specified id.

## Content

The solution includes two projects. **ClassLibrary** and **ProductFrontend**.

### ClassLibrary

The ClassLibrary contains logic that grabs data from the database, optimizes it and sends it to the frontend.

### ProductFrontend

The ProductFrontend is a .NET MVC app which purpose present optimized table data to the user.
