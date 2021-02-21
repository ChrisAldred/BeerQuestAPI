# Beer Quest API
Beer Quest API is a queryable API that exposes the Beer Quest data set, available at https://datamillnorth.org/download/leeds-beer-quest/c8884f6c-84a0-4a54-9c71-c5016bf4d878/leedsbeerquest.csv.

## Description
Available filters are Name, Category, and Tag. Whilst you can paginate the response by setting an offset and limit. Results are ordered by Name alphabetically ascending.

### Name
You can filter on Name, which will match on partial and complete words. It is a case insensitive search, but does require symbols and accents to be specified correctly.

### Category
You can filter on Category, which will match on partial and complete words. It is a case insensitive search, but does require symbols and accents to be specified correctly.

### Tag
You can filter on Tag, specifying one tag in the request. It  will match on partial and complete words. It is a case insensitive search, but does require symbols and accents to be specified correctly.

### Offset and Limit
Pagination is available on the result set through specifying offset and limit variables in the request. The return object specifies what offset and limit was requested, as well as the total number of rows in the, unfiltered, data set.
