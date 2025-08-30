# StockWatch-Pro: Real-Time Stock Monitoring API

## 🚀 Overview

StockWatch-Pro is a comprehensive .NET 8 Web API that provides real-time stock monitoring with price alerts, portfolio management, and social features. The application allows users to track stocks, set custom price alerts, manage portfolios, and engage with other users through comments.

## ✨ Key Features

### 🔔 Real-Time Price Alerts
- **Custom Price Alerts**: Set alerts for when stocks reach specific price thresholds
- **Real-Time Monitoring**: Background service continuously monitors stock prices
- **Instant Notifications**: Get notified immediately when price conditions are met
- **Alert Types**: Support for both "Price Above" and "Price Below" alerts
- **External API Integration**: Fetches real-time prices from Yahoo Finance API

### 📊 Stock Management
- **Stock Information**: Track stock symbols, company names, purchase prices, dividends
- **Market Data**: Monitor market cap, industry classification
- **Stock Search**: Query stocks with filtering and pagination
- **CRUD Operations**: Full create, read, update, delete functionality

### 💼 Portfolio Management
- **Personal Portfolios**: Add/remove stocks to your personal watchlist
- **Portfolio Tracking**: Monitor your selected stocks
- **User-Specific**: Each user has their own portfolio

### 💬 Social Features
- **Stock Comments**: Add comments to stocks for discussion
- **Community Engagement**: Share insights and opinions
- **Comment Management**: Edit and delete your comments

### 🔐 Authentication & Security
- **JWT Authentication**: Secure token-based authentication
- **User Registration**: Create new accounts with email verification
- **Role-Based Access**: Admin and User roles
- **Password Security**: Strong password requirements

## 🏗️ Architecture

### Technology Stack
- **.NET 8** with ASP.NET Core Web API
- **PostgreSQL** database with Entity Framework Core
- **JWT Authentication** for secure API access
- **Identity Framework** for user management
- **Swagger/OpenAPI** for API documentation
- **CORS** enabled for cross-origin requests
- **Background Services** for real-time monitoring

### Project Structure
```
StockWatch-Pro/
├── Controllers/          # API endpoints
├── Models/              # Database entities
├── Dtos/                # Data transfer objects
├── Interfaces/          # Repository and service contracts
├── Repository/          # Data access layer
├── Services/            # Business logic services
├── Mappers/             # Object mapping utilities
├── Data/                # Database context and configuration
├── Migrations/          # Database migrations
├── Extensions/          # Custom extensions
├── Helpers/             # Utility classes
└── Constants/           # Application constants
```

## 📋 API Endpoints

### Authentication
- `POST /api/Account/register` - Register new user
- `POST /api/Account/login` - User login

### Stocks
- `GET /api/Stock` - Get all stocks (with filtering)
- `GET /api/Stock/{id}` - Get stock by ID
- `POST /api/Stock` - Create new stock
- `PUT /api/Stock/{id}` - Update stock
- `DELETE /api/Stock/{id}` - Delete stock

### Portfolios
- `GET /api/Portfolio` - Get user's portfolio
- `POST /api/Portfolio/{stockId}` - Add stock to portfolio
- `DELETE /api/Portfolio/{stockId}` - Remove stock from portfolio

### Comments
- `GET /api/Comment` - Get all comments
- `GET /api/Comment/{id}` - Get comment by ID
- `POST /api/Comment/{stockId}` - Add comment to stock
- `PUT /api/Comment/{commentId}` - Update comment
- `DELETE /api/Comment/{commentId}` - Delete comment

### Price Alerts
- `GET /api/PriceAlert` - Get user's price alerts
- `GET /api/PriceAlert/{id}` - Get specific alert
- `POST /api/PriceAlert` - Create new price alert
- `DELETE /api/PriceAlert/{id}` - Delete alert
- `PUT /api/PriceAlert/{id}/toggle` - Toggle alert active status

## 🗄️ Database Schema

### Core Entities
- **AppUser**: User accounts with authentication
- **Stock**: Stock information and market data
- **Portfolio**: User-stock relationships (many-to-many)
- **Comment**: User comments on stocks
- **PriceAlert**: User price alert configurations

### Relationships
- Users can have multiple portfolios (stocks)
- Users can have multiple price alerts
- Users can have multiple comments
- Stocks can have multiple comments
- Stocks can have multiple price alerts

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- PostgreSQL database
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd StockWatch-Pro
   ```

2. **Configure the database**
   - Update the connection string in `appsettings.json`
   - Ensure PostgreSQL is running
   - Run database migrations

3. **Install dependencies**
   ```bash
   dotnet restore
   ```

4. **Build the project**
   ```bash
   dotnet build
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the API**
   - API: `http://localhost:5110`
   - Swagger UI: `http://localhost:5110/swagger`

### Configuration

#### Database Connection
Update `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "WebApiDatabase": "Host=localhost; Database=stockmanagement; Username=postgres; Password=your_password"
  }
}
```

#### JWT Settings
```json
{
  "JWT": {
    "Issuer": "http://localhost:5110",
    "Audience": "http://localhost:5110",
    "SigningKey": "your-secure-signing-key-here"
  }
}
```

## 🔧 Development

### Adding New Features
1. Create models in `Models/` directory
2. Add DTOs in `Dtos/` directory
3. Create interfaces in `Interfaces/` directory
4. Implement repositories in `Repository/` directory
5. Add controllers in `Controllers/` directory
6. Register services in `Program.cs`

### Database Migrations
```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update
```

### Testing the API
Use the provided `api.http` file or Swagger UI to test endpoints.

## 🔔 Real-Time Features

### Price Alert Monitoring
The application includes a background service that:
- Monitors stock prices every minute
- Checks active price alerts
- Triggers notifications when conditions are met
- Marks alerts as triggered and deactivates them

### External API Integration
- **Yahoo Finance API**: Primary source for real-time stock prices
- **Fallback System**: Mock data when external API is unavailable
- **Error Handling**: Graceful degradation on API failures

## 🔐 Security Features

- **JWT Token Authentication**: Secure API access
- **Password Requirements**: Minimum 8 characters with complexity
- **Role-Based Authorization**: Admin and User roles
- **CORS Configuration**: Configurable cross-origin requests
- **Input Validation**: Comprehensive request validation

## 📊 Performance Considerations

- **Repository Pattern**: Efficient data access
- **Async/Await**: Non-blocking operations
- **Entity Framework**: Optimized database queries
- **Background Services**: Efficient real-time monitoring
- **Caching**: Consider implementing Redis for production

## 🚀 Deployment

### Docker Support
The project includes Docker configuration for containerized deployment.

### Production Considerations
- Use strong JWT signing keys
- Configure proper CORS policies
- Set up production database
- Implement logging and monitoring
- Configure external stock API keys

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 🆘 Support

For support and questions:
- Create an issue in the repository
- Check the Swagger documentation
- Review the API endpoints

---

**StockWatch-Pro** - Your comprehensive solution for real-time stock monitoring and portfolio management! 📈
