# Interview Project

This project is an assignment given to me to evaluate my development skills.
The project uses .NET framework 6.0.

The application manages "inventory" items, each with a barcode, name, and price.

Capabilities:
- Loads inventory data from a specified csv file on startup.
- Hosts a simple, CRUD-capable REST API using ASP.NET for managing inventory items.
- Stores and retrieves all data from a SQLite3 database (located at `Database/inventory.db`).

## API Endpoints and usage

### Endpoints

```
GET    /api/inventory/     Returns a list of all inventory items
GET    /api/inventory/{id} Returns a single inventory item by id
POST   /api/inventory/     Creates a new inventory item
PUT    /api/inventory/{id} Updates an existing inventory item
DELETE /api/inventory/{id} Deletes an existing inventory item
```

The `GET` and `DELETE` endpoints are (hopefully) self-explanatory.
The endpoints that require data to be submitted, including the `POST` and `PUT` endpoints, all require a full `InventoryItem` object to be submitted, including the `id` property.

An example `InventoryItem` in JSON format:

```json
{
    "id": 1,
    "barcode": "1234567890123",
    "name": "Test Item",
    "price": 1.99
}
```

### CSV Files

There are two provided csv files, `example_data.csv` and `bad_data.csv`:
- `example_data.csv` contains a list of valid inventory items that can be loaded without issue.
- `bad_data.csv` contains a list of invalid inventory items that not be parsable by the application.
Used for testing error handling.

To load either of these files, provide their path as a command line argument:

I recommend setting `skip:true, verbose:true` at `Program.cs:33` when using the `bad_data.csv` file for a more comprehensive output.
