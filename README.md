## Steps before development
- create appsettings.Development.json with the given content.
{
  "ConnectionStrings": {
    "GoOnlineDbConnection": "Server=;Database=;User Id=;Password=;TrustServerCertificate=True;"
  }
}
- Then provide the appropriate data

## Steps before integration tests
- Run docker desktop

## Steps before dockerization
- Build and run the Docker container with the following command:
  - `docker-compose up
- Open API at: http://localhost:5020/swagger/index.html
