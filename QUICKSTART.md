# StockWatch Pro - Quick Start Guide (Java/Spring Boot)

## Prerequisites

- **Java 21**: [Download from Oracle](https://www.oracle.com/java/technologies/downloads/#java21) or use OpenJDK
- **Maven 3.8+**: [Download from Apache](https://maven.apache.org/download.cgi)
- **PostgreSQL 12+**: [Download from PostgreSQL](https://www.postgresql.org/download/)
- **Git**: For version control

## Project Setup

### 1. Database Setup

```bash
# Connect to PostgreSQL
psql -U postgres

# Create database
CREATE DATABASE stockmanagement;

# Exit psql
\q
```

Or use the connection string directly (if you set password in environment):
```bash
createdb stockmanagement
```

### 2. Clone and Navigate

```bash
cd StockWatch-Pro
git checkout development  # Already on development branch
```

### 3. Build the Project

```bash
# Clean and install all dependencies
mvn clean install

# Or just build without running tests
mvn clean package -DskipTests
```

### 4. Run the Application

```bash
# Using Maven Spring Boot plugin
mvn spring-boot:run -Dspring-boot.run.arguments="--spring.profiles.active=dev"

# Or create an executable JAR and run it
mvn clean package
java -jar target/stockwatch-pro-1.0.0.jar
```

The application will start on `http://localhost:8080/api`

## First Steps

### 1. Access Swagger UI

Open your browser and navigate to:
```
http://localhost:8080/api/swagger-ui.html
```

### 2. Register a New User

```bash
curl -X POST http://localhost:8080/api/account/register \
  -H "Content-Type: application/json" \
  -d '{
    "username": "testuser",
    "email": "test@example.com",
    "password": "SecurePass123"
  }'
```

**Response** (save the token):
```json
{
  "username": "testuser",
  "email": "test@example.com",
  "token": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9..."
}
```

### 3. Login to Existing Account

```bash
curl -X POST http://localhost:8080/api/account/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "SecurePass123"
  }'
```

### 4. Create a Stock (Admin Only)

First, promote your user to ADMIN in the database:
```sql
UPDATE users SET roles = ARRAY['ROLE_ADMIN'] WHERE email = 'test@example.com';
```

Then create a stock:
```bash
curl -X POST http://localhost:8080/api/stock \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -d '{
    "symbol": "AAPL",
    "companyName": "Apple Inc.",
    "purchase": 150.00,
    "lastDiv": 0.24,
    "industry": "Technology",
    "marketCap": 2850000000000
  }'
```

### 5. Get All Stocks

```bash
curl -X GET http://localhost:8080/api/stock \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

### 6. Add Stock to Portfolio

```bash
curl -X POST http://localhost:8080/api/portfolio \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -d '{
    "stockId": 1
  }'
```

### 7. Create a Price Alert

```bash
curl -X POST http://localhost:8080/api/pricealert \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -d '{
    "stockId": 1,
    "targetPrice": 160.00,
    "alertType": "PRICE_ABOVE"
  }'
```

### 8. Get User's Active Alerts

```bash
curl -X GET http://localhost:8080/api/pricealert/user/active \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

## Project Structure

```
src/main/
├── java/com/stockwatch/stockwatchpro/
│   ├── StockWatchProApplication.java        # Main entry point
│   ├── config/SecurityConfig.java           # Security configuration
│   ├── controllers/                         # REST API endpoints
│   ├── models/                              # JPA entities
│   ├── dtos/                                # Data transfer objects
│   ├── repositories/                        # Data access layer
│   ├── services/                            # Business logic
│   └── security/                            # JWT and auth utilities
└── resources/
    ├── application.properties               # Default config
    ├── application-dev.properties           # Development config
    └── db/changelog/                        # Database migrations
```

## Configuration

Edit `src/main/resources/application.properties` to change:

```properties
# Server port
server.port=8080

# Database connection
spring.datasource.url=jdbc:postgresql://localhost:5432/stockmanagement
spring.datasource.username=postgres
spring.datasource.password=21184114

# JWT settings
jwt.signing.key=YOUR_SECRET_KEY
jwt.expiration=30  # days

# Logging level
logging.level.com.stockwatch.stockwatchpro=DEBUG
```

## Common Commands

```bash
# Build without running tests
mvn clean package -DskipTests

# Run tests
mvn test

# Run specific test class
mvn test -Dtest=StockRepositoryTest

# Clean build artifacts
mvn clean

# Install dependencies
mvn install

# Check for dependency updates
mvn versions:display-dependency-updates

# Generate project documentation
mvn site

# Package as executable JAR
mvn clean package

# Deploy to production
java -Dspring.profiles.active=prod -jar target/stockwatch-pro-1.0.0.jar
```

## Troubleshooting

### Application won't start
- Check PostgreSQL is running: `pg_isready`
- Verify database exists: `psql -l | grep stockmanagement`
- Check logs: Look for stack traces in console output

### Database errors
- Clear Liquibase history: `DELETE FROM databasechangelog;`
- Restart migrations: Application will auto-run on startup

### JWT/Authentication issues
- Token format should be: `Authorization: Bearer <token>`
- Token expires after 30 days (configurable in properties)
- Check system time is correct (JWT is time-sensitive)

### Port already in use
```bash
# Change port in application.properties
server.port=8081

# Or find and kill the process using port 8080
lsof -ti:8080 | xargs kill -9
```

## IDE Setup

### IntelliJ IDEA
1. Open project root directory
2. Mark `src/main/java` as Sources Root
3. Mark `src/main/resources` as Resources Root
4. Maven should auto-detect pom.xml
5. Install "Lombok" plugin for annotation processing

### VS Code
1. Install "Extension Pack for Java" by Microsoft
2. Install "Spring Boot Extension Pack" by Pivotal
3. Maven will auto-detect configuration
4. Install "REST Client" extension for API testing

### Eclipse
1. Import as "Existing Maven Project"
2. Maven will auto-configure
3. Install "Spring IDE" plugin for better Spring support

## Next Steps

1. Read [MIGRATION_GUIDE.md](./MIGRATION_GUIDE.md) for detailed architecture
2. Explore API in Swagger UI: `http://localhost:8080/api/swagger-ui.html`
3. Review database schema in PostgreSQL
4. Run sample API requests (see examples above)
5. Implement additional features as needed

## Support & Documentation

- **Spring Boot**: https://spring.io/projects/spring-boot
- **Spring Data JPA**: https://spring.io/projects/spring-data-jpa
- **Spring Security**: https://spring.io/projects/spring-security
- **PostgreSQL**: https://www.postgresql.org/docs/
- **JWT Documentation**: https://tools.ietf.org/html/rfc7519

## Performance Testing

Run load tests to verify 100ms latency target:

```bash
# Using Apache Bench (if installed)
ab -n 1000 -c 10 http://localhost:8080/api/stock

# Or use JMeter for more detailed analysis
# Download from: https://jmeter.apache.org/
```

## Production Deployment

For production deployment:

1. Set profile: `--spring.profiles.active=prod`
2. Use environment variables for sensitive data
3. Enable HTTPS/SSL
4. Configure connection pooling
5. Set up monitoring and logging
6. Use load balancer
7. Enable database backups
8. Set up auto-scaling (if using cloud)

See [MIGRATION_GUIDE.md](./MIGRATION_GUIDE.md) for more details.
