# StockWatch Pro - C# to Java Migration Guide

## Overview
This document outlines the successful migration of StockWatch Pro from a C# ASP.NET Core application to a Java Spring Boot 3.x application.

## Technology Stack

### Original (C#)
- **Language**: C# with .NET 8.0
- **Framework**: ASP.NET Core 8.0
- **Database**: PostgreSQL with Entity Framework Core
- **Authentication**: JWT Bearer Tokens
- **ORM**: Entity Framework Core

### New (Java)
- **Language**: Java 21
- **Framework**: Spring Boot 3.3.0
- **Database**: PostgreSQL with JPA/Hibernate
- **Authentication**: JWT Bearer Tokens (JJWT)
- **ORM**: Spring Data JPA with Hibernate

## Project Structure

```
src/
├── main/
│   ├── java/com/stockwatch/stockwatchpro/
│   │   ├── StockWatchProApplication.java       # Spring Boot entry point
│   │   ├── config/                             # Configuration classes
│   │   │   └── SecurityConfig.java             # Security and CORS config
│   │   ├── controllers/                        # REST endpoints
│   │   │   ├── AccountController.java
│   │   │   ├── StockController.java
│   │   │   ├── PortfolioController.java
│   │   │   ├── CommentController.java
│   │   │   └── PriceAlertController.java
│   │   ├── models/                             # JPA Entity classes
│   │   │   ├── AppUser.java
│   │   │   ├── Stock.java
│   │   │   ├── Portfolio.java
│   │   │   ├── Comment.java
│   │   │   ├── PriceAlert.java
│   │   │   └── PortfolioId.java
│   │   ├── dtos/                               # Data Transfer Objects
│   │   │   ├── RegisterDto.java
│   │   │   ├── LoginDto.java
│   │   │   ├── CredentialTokenDto.java
│   │   │   ├── StockDto.java
│   │   │   ├── CreateStockRequestDto.java
│   │   │   ├── UpdateStockRequestDto.java
│   │   │   ├── CommentDto.java
│   │   │   ├── CreatePortfolioDto.java
│   │   │   ├── CreatePriceAlertDto.java
│   │   │   ├── PriceAlertDto.java
│   │   │   └── GenericResponse.java
│   │   ├── repositories/                       # Spring Data JPA repositories
│   │   │   ├── AppUserRepository.java
│   │   │   ├── StockRepository.java
│   │   │   ├── PortfolioRepository.java
│   │   │   ├── CommentRepository.java
│   │   │   └── PriceAlertRepository.java
│   │   ├── services/                           # Business logic services
│   │   │   ├── AppUserService.java
│   │   │   ├── TokenService.java
│   │   │   ├── StockService.java
│   │   │   ├── PortfolioService.java
│   │   │   ├── CommentService.java
│   │   │   └── PriceAlertService.java
│   │   ├── security/                           # Security and JWT utilities
│   │   │   ├── JwtUtil.java
│   │   │   ├── JwtAuthenticationFilter.java
│   │   │   └── CustomUserDetailsService.java
│   │   └── exceptions/                         # Custom exception classes
│   └── resources/
│       ├── application.properties              # Default properties
│       ├── application-dev.properties          # Development properties
│       └── db/changelog/                       # Liquibase migration scripts
│           ├── db.changelog-master.xml
│           ├── 01-init-schema.xml
│           └── 02-add-price-alerts.xml
└── pom.xml                                     # Maven build configuration
```

## Key Mapping Changes

### C# to Java Mappings

| C# Concept | Java Equivalent |
|-----------|-----------------|
| `public class Model : IdentityUser` | `@Entity` JPA class with `@OneToMany` relationships |
| `DbSet<T>` | `JpaRepository<T, ID>` |
| `async Task<T>` | `Optional<T>` or `List<T>` |
| Entity Framework migrations | Liquibase XML migrations |
| `IConfiguration` | `@Value` annotation or `Environment` |
| Dependency injection in constructor | Constructor injection with `@Autowired` or field injection |
| `[Required]` attributes | `@NotNull`, `@NotBlank` annotations |
| `var` implicit typing | Explicit types or Java 10+ `var` |

## Database Changes

### Migration from EF Core to JPA/Hibernate

1. **User Management**: Replaced ASP.NET Identity with custom AppUser entity + user_roles table
2. **Enum Handling**: `AlertType` enum directly in entity class
3. **Indexes**: Defined via `@Index` annotations on entities and in Liquibase
4. **Composite Keys**: Used `@IdClass` pattern for Portfolio (AppUserId, StockId)

### Liquibase Migrations

All database schemas are managed through Liquibase XML files:
- `01-init-schema.xml`: Creates users, stocks, portfolios, and comments tables
- `02-add-price-alerts.xml`: Creates price_alerts table with proper foreign keys

## Authentication & Security

### JWT Implementation

**C# Original**:
```csharp
var tokenHandler = new JwtSecurityTokenHandler();
var token = tokenHandler.CreateToken(tokenDescriptor);
```

**Java Equivalent**:
```java
String token = Jwts.builder()
    .claims(claims)
    .subject(subject)
    .signWith(getSigningKey(), SignatureAlgorithm.HS512)
    .compact();
```

### Security Configuration

- **Spring Security**: Configured via `SecurityConfig.java`
- **CORS**: Enabled for all origins with `CorsConfigurationSource`
- **JWT Filter**: Custom `JwtAuthenticationFilter` validates tokens on each request
- **Password Encoding**: BCryptPasswordEncoder for secure password storage

## API Endpoints

All endpoints remain functionally equivalent:

### Authentication
- `POST /api/account/register` - Register new user
- `POST /api/account/login` - Login user

### Stock Management
- `GET /api/stock` - Get all stocks
- `GET /api/stock/{id}` - Get stock by ID
- `GET /api/stock/symbol/{symbol}` - Get stock by symbol
- `GET /api/stock/industry/{industry}` - Get stocks by industry
- `GET /api/stock/search?companyName=X` - Search by company name
- `POST /api/stock` - Create new stock (ADMIN only)
- `PUT /api/stock/{id}` - Update stock (ADMIN only)
- `DELETE /api/stock/{id}` - Delete stock (ADMIN only)

### Portfolio Management
- `GET /api/portfolio` - Get user's portfolio
- `POST /api/portfolio` - Add stock to portfolio
- `DELETE /api/portfolio/{stockId}` - Remove stock from portfolio
- `GET /api/portfolio/check/{stockId}` - Check if stock in portfolio

### Comments
- `GET /api/comment/stock/{stockId}` - Get stock comments
- `GET /api/comment/{id}` - Get comment by ID
- `POST /api/comment` - Create comment
- `PUT /api/comment/{id}` - Update comment
- `DELETE /api/comment/{id}` - Delete comment

### Price Alerts
- `GET /api/pricealert/{id}` - Get alert by ID
- `GET /api/pricealert/user/active` - Get user's active alerts
- `GET /api/pricealert/user/all` - Get user's all alerts
- `GET /api/pricealert/stock/{stockId}/active` - Get stock's active alerts
- `POST /api/pricealert` - Create price alert
- `PATCH /api/pricealert/{id}?isActive=X` - Toggle alert status
- `DELETE /api/pricealert/{id}` - Delete alert

## Running the Application

### Prerequisites
- Java 21
- Maven 3.8+
- PostgreSQL 12+

### Setup

1. **Install Dependencies**:
   ```bash
   mvn clean install
   ```

2. **Configure Database** (application.properties):
   ```properties
   spring.datasource.url=jdbc:postgresql://localhost:5432/stockmanagement
   spring.datasource.username=postgres
   spring.datasource.password=YOUR_PASSWORD
   ```

3. **Run Application**:
   ```bash
   mvn spring-boot:run -Dspring-boot.run.arguments="--spring.profiles.active=dev"
   ```

4. **Access API**:
   - API Base: `http://localhost:8080/api`
   - Swagger UI: `http://localhost:8080/api/swagger-ui.html`

## Testing

### Database Migrations

Liquibase will automatically run migrations on application startup:
```
INFO: Successfully acquired change log lock
INFO: Creating database history table...
INFO: Running changelog: db/changelog/db.changelog-master.xml
```

### Key Differences from C#

1. **No automatic code-first migrations**: Liquibase requires explicit XML migration files
2. **Different timestamp handling**: Java uses `LocalDateTime` vs C# `DateTime`
3. **Type conversions**: Java BigDecimal for precise decimal values
4. **Generic collections**: Java generics are stricter than C# implicit types
5. **Session/Transaction management**: JPA handles this differently than EF Core

## Performance Considerations

### Optimizations Implemented

1. **Database Indexing**: Added indexes on frequently queried columns (symbol, company_name, user_id, stock_id)
2. **Lazy Loading**: JPA relationships configured with `FetchType.LAZY` to reduce N+1 queries
3. **Connection Pooling**: Spring Boot uses HikariCP by default
4. **Query Optimization**: Custom repository methods with @Query annotations for complex queries

### Monitoring

Enable detailed logging in `application-dev.properties`:
```properties
logging.level.org.hibernate.SQL=DEBUG
logging.level.org.hibernate.type.descriptor.sql.BasicBinder=TRACE
spring.jpa.properties.hibernate.format_sql=true
```

## Troubleshooting

### Common Issues

1. **Database Connection Fails**
   - Check PostgreSQL service is running
   - Verify connection string and credentials
   - Ensure database exists: `createdb stockmanagement`

2. **Liquibase Migration Errors**
   - Check XML syntax in migration files
   - Verify column types and constraints match
   - Clear Liquibase tables if needed: `DELETE FROM databasechangelog`

3. **JWT Token Issues**
   - Verify signing key length (minimum 32 characters)
   - Check token expiration settings
   - Ensure Authorization header format: `Bearer <token>`

4. **CORS Problems**
   - Verify CORS is enabled in SecurityConfig
   - Check allowed origins and methods
   - Browser console will show specific CORS errors

## Future Enhancements

1. **Async Processing**: Use `@Async` for long-running operations
2. **Caching**: Implement Spring Cache abstraction for frequently accessed data
3. **Real-time Updates**: WebSocket support for live stock price updates
4. **Message Queues**: RabbitMQ or Kafka for asynchronous alert notifications
5. **Rate Limiting**: Implement Spring Cloud Gateway or custom filters
6. **Monitoring**: Micrometer/Actuator for application metrics
7. **Testing**: Comprehensive JUnit 5 + Mockito test suite

## References

- [Spring Boot Documentation](https://spring.io/projects/spring-boot)
- [Spring Data JPA](https://spring.io/projects/spring-data-jpa)
- [Spring Security](https://spring.io/projects/spring-security)
- [Liquibase Documentation](https://docs.liquibase.com/)
- [JJWT (Java JWT)](https://github.com/jwtk/jjwt)
- [Hibernate ORM](https://hibernate.org/)
