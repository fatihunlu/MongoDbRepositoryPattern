# MongoDbRepositoryPattern

This project demonstrates how to implement the **Repository Pattern** using **.NET 8 Web API** with **MongoDB**.  

[Read Article](https://medium.com/@unlu-fa/mongodb-c-repository-pattern-made-simple-c14a31281ccf)

---

## Run MongoDB with Docker

Start MongoDB using Docker:

```bash
docker run -d --name mongo \
  -p 27017:27017 \
  -e MONGO_INITDB_ROOT_USERNAME=admin \
  -e MONGO_INITDB_ROOT_PASSWORD=secret \
  mongo:latest
```

## Connect with MongoDB Compass
```bash
mongodb://admin:secret@localhost:27017/?authSource=admin
```

## Run docker container
```bash
dotnet run
```

## Examples

### Create User

```bash
curl -X POST "https://localhost:7121/api/users" \
  -H "Content-Type: application/json" \
  -d '{"email":"demo@example.com","name":"Demo User"}' \
  -k
```

### Get All Users

```bash
curl -X GET "https://localhost:7121/api/users" \
  -H "Accept: application/json" \
  -k
```

