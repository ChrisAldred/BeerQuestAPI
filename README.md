# Beer Quest API
Beer Quest API is a queryable API that exposes the Beer Quest data set, available at https://datamillnorth.org/download/leeds-beer-quest/c8884f6c-84a0-4a54-9c71-c5016bf4d878/leedsbeerquest.csv.

## Description
Available filters are Name, Category, and Tag. Whilst you can paginate the response by setting an offset and limit. Results are ordered by Name alphabetically ascending.

## Installation
- Run BeerQuest.sql on your localhost T-SQL instnace
- Make sure Docker is installed and correctly set-up on your PC (Docker Desktop set-up can be found at https://docs.docker.com/desktop/)
- Use you favourite IDE to open and inspect the code before running it.
- Run the application and view the documentation at the base url or /index.html

## Return Object
```json
{
  "offset": 0,
  "limit": 0,
  "businessCollection": [
    {
      "id": 0,
      "name": "string",
      "category": "string",
      "url": "string",
      "date": "2021-02-21T14:20:16.418Z",
      "excerpt": "string",
      "thumbnail": "string",
      "lat": 0,
      "lng": 0,
      "address": "string",
      "phone": "string",
      "twitter": "string",
      "starsBeer": 0,
      "starsAtmosphere": 0,
      "starsAmenities": 0,
      "starsValue": 0,
      "tags": "string",
      "totalRows": 0
    }
  ]
}
```

## Query Parameters
### Name
You can filter on Name, which will match on partial and complete words. It is a case insensitive search, but does require symbols and accents to be specified correctly.

### Category
You can filter on Category, which will match on partial and complete words. It is a case insensitive search, but does require symbols and accents to be specified correctly.

### Tag
You can filter on Tag, specifying one tag in the request. It  will match on partial and complete words. It is a case insensitive search, but does require symbols and accents to be specified correctly.

### Offset and Limit
Pagination is available on the result set through specifying offset and limit variables in the request. The return object specifies what offset and limit was requested, as well as the total number of rows in the, unfiltered, data set.
