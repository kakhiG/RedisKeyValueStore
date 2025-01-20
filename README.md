# RedisKeyValueStore

## API Endpoints

- GET /api/KeyValue/{key} - Get value by key
- PUT /api/KeyValue/{key} - Set value for key
- DELETE /api/KeyValue/{key} - Delete value by key
- POST /api/KeyValue/bulk-get - Get multiple values
- PUT /api/KeyValue/{key}/atomic - Atomic update

## Test Data

The application seeds test data on startup including:
- Users (user1, user2, user3)
- Products (product1, product2, product3)
- Temporary values (temp:1, temp:2)
- Counter (counter:visits)
