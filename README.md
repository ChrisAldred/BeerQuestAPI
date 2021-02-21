# Beer Quest API
Beer Quest API is a queryable API that exposes the Beer Quest data set, available at https://datamillnorth.org/download/leeds-beer-quest/c8884f6c-84a0-4a54-9c71-c5016bf4d878/leedsbeerquest.csv.

## Description
Available filters are Name, Category, and Tag. Whilst you can paginate the response by setting an offset and limit. Results are ordered by Name alphabetically ascending.

## Installation
- Run BeerQuest.sql on your localhost T-SQL instnace
- Make sure Docker is installed and correctly set-up on your PC (Docker Desktop set-up can be found at https://docs.docker.com/desktop/)
- Use you favourite IDE to open and inspect the code before running it.
- Run the application and view the documentation at the base url or /index.html

## Usage
There is one POST endpoint "/api/business" which takes a request body that conatins the following nullable attributes:
```json
{
  "name": "atlas",
  "category": "bar",
  "tag": "coffee",
  "offset": 0,
  "limit": 1000
}
```
Whilst it is highly unusual to implement a POST endpoint which returns large amounts of data, this avoids violating the HTTP 1.1 spec. THis specifies that no indentifiable information should be placed in the request body of a GET statement, and also allowing easily nullable request parameters wiithout duplicating routes, and methods on the controller. Further reading can be found at https://tools.ietf.org/html/rfc7231#page-24

## Return Object
### Populated Array
```json
{
  "offset": 0,
  "limit": 0,
  "businessCollection": [
    {
      "id": 2,
      "name": "\"golf\" cafe bar",
      "category": "bar reviews",
      "url": "http://leedsbeer.info/?p=1382",
      "date": "2013-04-27T15:44:22+01:00",
      "excerpt": "FORE! You can play \"golf\" here and enjoy a nice bottled ale. ",
      "thumbnail": "http://leedsbeer.info/wp-content/uploads/2013/04/20130422_204442.jpg",
      "lat": 53.793495,
      "lng": -1.5478653,
      "address": "1 Little Neville Street, Granary Wharf, Leeds LS1 4ED",
      "phone": "0113 244 4428",
      "twitter": "GolfCafeBar",
      "starsBeer": 0,
      "starsAtmosphere": 2.5,
      "starsAmenities": 3.5,
      "starsValue": 2.5,
      "tags": "beer garden,coffee,food,free wifi,sports",
      "totalRows": 242
    },
    {
      "id": 5,
      "name": "314 in progress",
      "category": "bar reviews",
      "url": "http://leedsbeer.info/?p=3048",
      "date": "2015-03-24T20:31:30+00:00",
      "excerpt": "This fashionable cocktail bar & club isn't really our scene but has surprisingly good beer.",
      "thumbnail": "http://leedsbeer.info/wp-content/uploads/2015/03/IMG_20150209_175103005.jpg",
      "lat": 53.80072,
      "lng": -1.5483344,
      "address": "25 Great George Street, Leeds LS1 3AL",
      "phone": "0113 397 1337",
      "twitter": "314Leeds",
      "starsBeer": 0,
      "starsAtmosphere": 2.5,
      "starsAmenities": 1.5,
      "starsValue": 2,
      "tags": "dance floor",
      "totalRows": 242
    }
  ]
}
```
### Empty Array
```json
{
  "offset": 0,
  "Limit": 100,
  "businessCollection": []
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
