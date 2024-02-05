# ERP Application - Trucks Management API

The Trucks Management API is the first REST API module of the ERP application, designed to manage Trucks data. This module is part of a broader system that will include additional endpoints for managing various resources such as employees, factories, and customers.

Unit tests not included.

## Truck Requirements

Each Truck in the system must adhere to the following specifications:

- **Unique Code**: Each truck must be assigned a unique alphanumeric code by the user.
- **Name**: Every truck must have a name.
- **Status**: The status of a truck must belong to the following set:
  - "Out Of Service" (can be set regardless of the current status)
  - "Loading"
  - "To Job"
  - "At Job"
  - "Returning"
  - Transition rules:
    - "Out Of Service" status can be set regardless of the current status of the truck.
    - Each status can be set if the current status is "Out of Service."
    - The remaining statuses can only be changed in the following order: Loading -> To Job -> At Job -> Returning.
    - When a truck has the "Returning" status, it can transition back to "Loading."
- **Description**: Trucks may have an optional description.

## CRUD Operations

Users have the ability to perform CRUD (Create, Read, Update, Delete) operations on trucks. This includes the ability to query a filtered and sorted list of trucks.

### API Endpoints

- **Create Truck**: `POST /api/trucks`
- **Read Truck**: `GET /api/trucks/{code}`
- **Update Truck**: `PUT /api/trucks/{code}`
- **Delete Truck**: `DELETE /api/trucks/{code}`
- **Get All Trucks**: `GET /api/trucks`

### Querying Trucks

Users can query the list of trucks with various filters and sorting options.

#### Query Parameters

- `Name`: Filter by truck name.
- `Status`: Filter by truck status.
- `Description`: Filter by truck description.
- `SortBy`: Sort the results by a specific field (e.g., "Code", "Name", "Status"). Add '-' for descending sorting (e. g. "-Name").

## Example Usage

### Create Truck

```http
POST /api/trucks
{
  "code": "T123",
  "name": "Truck One",
  "status": 0,
  "description": "A description of the truck."
}
