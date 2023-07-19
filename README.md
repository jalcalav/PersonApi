# PersonApi

.Net 6 Web Api demo.

## How To Execute

The following command will run this demo with docker-compose:

```bash
docker-compose up
```

This action will run two containers:

- person_db: PostgreSQL Database Server mapping the host port 5432 to the container port 5432. The Database schema is initialized automatically by .Net 6 migrations.
- person-service: Web Api service implementation mapping the host port 5009 to the container port 80.

Once running, you can open the service Swagger using the URL [http://localhost:5009/swagger/index.html](http://localhost:5009/swagger/index.html).