# E-Commerce Platform

A full-stack e-commerce web application built with **ASP.NET Core 9.0**, featuring a RESTful API backend and an MVC frontend with complete shopping cart, order management, and admin dashboard functionality.

---

## Project Idea

This project was developed as a practical, end-to-end e-commerce solution that demonstrates real-world software engineering practices. The idea behind it was to build a complete online shopping platform where customers can browse products, manage their shopping carts, place orders, and track deliveries, while administrators have full control over product inventory, category management, user accounts, and order fulfillment.

The platform supports two user roles: **Customers** who can register, browse, shop, and manage their orders, and **Admins** who can manage the entire store operations through a dedicated dashboard. The system handles the complete order lifecycle from placement through shipment to delivery, including automatic stock management that decreases product quantities when orders are placed and reverts them when orders are cancelled.

The project emphasizes separation of concerns between the API and presentation layers, with the MVC frontend communicating with the REST API entirely through HTTP/JSON calls, making it a realistic example of a decoupled client-server architecture.

---

## Technology Stack

### Backend (E-Commerce.API)

| Technology | Version | Purpose |
|------------|---------|---------|
| .NET | 9.0 | Runtime framework |
| ASP.NET Core Web API | 9.0 | RESTful API framework |
| Entity Framework Core | 9.0 | ORM and data access layer |
| SQL Server | — | Relational database |
| JWT Bearer Authentication | — | Token-based stateless authentication |
| Swashbuckle (Swagger) | 6.6.2 | Auto-generated API documentation |
| Newtonsoft.Json | 13.0.4 | JSON serialization |

### Frontend (E-Commerce.MVC)

| Technology | Purpose |
|------------|---------|
| ASP.NET Core MVC | Server-side rendering framework |
| Razor Views | Dynamic page rendering with strongly-typed models |
| Bootstrap | Responsive UI framework |
| jQuery | Client-side scripting and AJAX |
| jQuery Validation | Client-side form validation |
| HttpClientFactory | HTTP communication with the API |
| Session State | Server-side user authentication state |

---

## Architecture

The application follows a **layered architecture** pattern with a clear separation between two independent projects communicating via HTTP/JSON:

```
E-Commerce.MVC (Frontend)               E-Commerce.API (Backend)
┌──────────────────────────┐            ┌──────────────────────────┐
│  Controllers             │            │  Controllers             │
│  (Accounts, Home,        │            │  (Accounts, Products,    │
│   Products, Carts,       │   HTTP     │   Categories, Carts,     │
│   Orders, Admin)         │   JSON     │   Orders, Users)         │
│         │                │◄──────────►│         │                │
│  Services                │            │  Services                │
│  (API Service Wrappers)  │            │  (Business Logic Layer)  │
│         │                │            │         │                │
│  Views (Razor)           │            │  Unit of Work            │
│  (HTML/CSS/JS)           │            │         │                │
└──────────────────────────┘            │  Generic Repository      │
                                        │         │                │
                                        │  EF Core / SQL Server   │
                                        └──────────────────────────┘
```

### Design Patterns Used

| Pattern | Implementation |
|---------|---------------|
| **Repository Pattern** | `GenericRepository<T>` provides CRUD operations for all entities |
| **Unit of Work Pattern** | `UOW` class aggregates all repositories sharing a single `DbContext` |
| **Service Layer Pattern** | Interface-based services (`IProductService` / `ProductService`) encapsulate business logic |
| **DTO Pattern** | Separate DTOs for Create, Update, and Read operations per entity to control data exposure |
| **Dependency Injection** | All services, UoW, and repositories registered via ASP.NET Core DI container |
| **HttpClient Factory** | Named HTTP client for type-safe API communication from MVC to API |

---

## Features

### Authentication & Authorization

- User registration with email and username uniqueness validation
- Login with email or username and password credentials
- JWT token generation with claims (Name, Email, Role)
- Role-based access control with **Customer** and **Admin** roles
- OTP verification (6-digit code with session-based expiration)
- Session-based authentication state on the MVC frontend
- Anti-forgery token protection on all POST form submissions
- Admin-only dashboard pages with role-based access checks

### User Management

- View all users and get user details by ID
- User profile page with personal information display
- Add, update, and delete user accounts (Admin)
- Change password functionality with current password verification

### Product Management

- Full CRUD operations for products
- Product image upload support (JPEG, PNG, WebP up to 5MB)
- Products organized by categories with filtering
- Stock quantity management (increase and decrease)
- Price validation (range: 0.01 - 1,000,000)
- Stock validation (range: 0 - 100,000)

### Category Management

- Full CRUD operations for categories
- 10 pre-seeded categories: Electronics, Clothing, Home Appliances, Books, Beauty Products, Sports Equipment, Toys & Games, Automotive Parts, Health & Personal Care, Office Supplies
- Filter products by category ID or name

### Shopping Cart

- Automatic cart creation upon user registration
- Add products to cart with stock availability validation
- View all cart items with product details and images
- Update item quantities in cart
- Remove individual items or clear entire cart
- Real-time cart total calculation

### Order Processing

- Place orders from a single product or from the entire cart
- Complete checkout flow with order summary
- Automatic stock deduction when orders are placed
- Order status tracking: Pending, Shipped, Delivered, Cancelled
- Order cancellation with automatic stock quantity restoration
- Order history with status-based filtering
- Detailed order view with all items and product information

### Admin Dashboard

- Centralized admin panel for store management
- Product management (Create, Read, Update, Delete with image upload)
- Category management (CRUD operations)
- User management (view all users, delete accounts)
- Order management (view all orders, update status, view details)
- Order rejection with automatic stock reversion

---

## Database Design

### Entity Relationship Diagram

```
User (1) ──────────────< (N) Order
  │                         │
  │ (1:1)                   │ (1:N)
  └──── Cart (1) ──────────< (N) CartItem >─────< (N) Product <───── (1) Category
                                                           │
                                                      (1:N) │
                                                 OrderItem <──── Order
```

### Entities

| Entity | Primary Key | Key Fields |
|--------|------------|------------|
| **User** | UserId | UserName, UserEmail, UserPassword, UserFullName, UserAddress, UserPhoneNumber, UserRole |
| **Product** | ProductId | ProductName, ProductDescription, ProductPrice, ProductStockQuantity, ProductImage, CategoryId (FK) |
| **Category** | CategoryId | CategoryName, CategoryDescription |
| **Cart** | CartId | UserId (FK) |
| **CartItem** | CartItemId | CartId (FK), ProductId (FK), Quantity |
| **Order** | OrderId | UserId (FK), OrderDate, Status, TotalAmount |
| **OrderItem** | OrderItemId | OrderId (FK), ProductId (FK), Quantity, UnitPrice |

### Enums

```csharp
public enum UserRoles { Customer = 0, Admin = 1 }

public enum CategoriesCollections {
    Electronics = 1, Clothing = 2, HomeAppliances = 3,
    Books = 4, BeautyProducts = 5, SportsEquipment = 6,
    ToysAndGames = 7, AutomotiveParts = 8,
    HealthAndPersonalCare = 9, OfficeSupplies = 10
}

public enum OrderStatus { Pending = 0, Shipped = 1, Delivered = 2, Cancelled = 3 }
```

---

## API Endpoints

### Accounts

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | `/api/Accounts/Register` | No | Register a new user and return JWT token |
| POST | `/api/Accounts/Login` | No | Authenticate user and return JWT token |
| DELETE | `/api/Accounts/Logout/{id}` | Yes | Log out user |
| POST | `/api/Accounts/ChangePassword` | No | Change user password |

### Products

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Products/GetAllProducts` | List all products |
| GET | `/api/Products/GetProductById/{id}` | Get product by ID |
| POST | `/api/Products/AddProduct` | Create product with image upload |
| PUT | `/api/Products/UpdateProduct` | Update product with optional image |
| PUT | `/api/Products/UpdateProductQuantity` | Decrease product stock |
| PUT | `/api/Products/IncreaseProductStock` | Increase product stock |
| DELETE | `/api/Products/DeleteProduct/{id}` | Delete product |
| GET | `/api/Products/GetAllProductsByCategoryId/{id}` | Filter products by category |
| GET | `/api/Products/GetAllProductsByCategoryName/{name}` | Filter by category name |

### Categories

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Categories/GetAllCategories` | List all categories |
| GET | `/api/Categories/GetCategoryById/{id}` | Get category by ID |
| GET | `/api/Categories/GetCategoryByName/{name}` | Get category by name |
| POST | `/api/Categories/AddCategory` | Create category |
| PUT | `/api/Categories/UpdateCategory` | Update category |
| DELETE | `/api/Categories/DeleteCategory/{id}` | Delete category |

### Carts & Cart Items

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Carts/GetCartById/{id}` | Get cart by ID |
| POST | `/api/Carts/AddCart` | Create new cart |
| GET | `/api/Carts/GetAllCartsByUserId/{userId}` | Get cart by user |
| POST | `/api/CartItems/AddCartItem` | Add item to cart |
| PUT | `/api/CartItems/UpdateCartItem` | Update cart item quantity |
| DELETE | `/api/CartItems/DeleteCartItem/{id}` | Remove item from cart |
| GET | `/api/CartItems/GetCartItemsByCartId/{cartId}` | Get all items in cart |

### Orders & Order Items

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Orders/GetAllOrders` | List all orders |
| GET | `/api/Orders/GetOrderById/{id}` | Get order with details |
| POST | `/api/Orders/AddOrder` | Create new order |
| PUT | `/api/Orders/UpdateOrder` | Update order status |
| GET | `/api/Orders/GetAllOrdersByUserId/{userId}` | Get orders by user |
| GET | `/api/OrderItems/GetOrderItemsByOrderId/{id}` | Get items in order |

### Users

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Users/GetAllUsers` | List all users |
| GET | `/api/Users/GetUserById/{id}` | Get user by ID |
| PUT | `/api/Users/UpdateUser` | Update user |
| DELETE | `/api/Users/DeleteUser/{id}` | Delete user |

---

## Project Structure

```
E-Commerce/
├── E-Commerce.API/                          # REST API Backend
│   ├── Controllers/                         # 8 API controllers
│   ├── DTOs/                                # Data Transfer Objects (20+ files)
│   │   ├── UserDTOs/, ProductDTOs/, CategoryDTOs/
│   │   ├── CartDTOs/, CartItemDTOs/
│   │   └── OrderDTOs/, OrderItemDTOs/
│   ├── Models/                              # 7 entity models
│   ├── Data/                                # EF Core DbContext
│   ├── Repositories/                        # Generic repository pattern
│   ├── UnitOfWork/                          # Unit of Work pattern
│   ├── Services/                            # Business logic layer
│   │   ├── Interfaces/                      # 8 service interfaces
│   │   └── Implementations/                 # 8 service implementations
│   ├── Helpers/                             # JWT configuration helper
│   ├── Filters/                             # Swagger authorization filter
│   ├── Migrations/                          # EF Core database migrations
│   ├── Program.cs                           # Application entry point
│   ├── appsettings.json                     # Configuration
│   └── E-Commerce.API.csproj                # Project file
│
└── E-Commerce.MVC/                          # MVC Frontend
    ├── Controllers/                         # 6 MVC controllers
    ├── DTOs/                                # View models and API DTOs
    ├── Models/                              # Shared entity models
    ├── Services/                            # API service wrappers
    │   ├── Interfaces/                      # 6 service interfaces
    │   └── Implementations/                 # 6 service implementations
    ├── Helpers/                             # OTP helper utility
    ├── Views/                               # Razor views
    │   ├── Home/, Accounts/, Products/
    │   ├── Carts/, Orders/, Admin/
    │   └── Shared/
    ├── wwwroot/                             # Static assets
    │   ├── css/                             # Feature-organized CSS
    │   ├── js/                              # Custom JavaScript
    │   └── lib/                             # Bootstrap, jQuery
    ├── Program.cs                           # Application entry point
    └── E-Commerce.MVC.csproj                # Project file
```

---

## Quick Start

```bash
git clone https://github.com/AzizNader1/E-Commerce.git
cd E-Commerce.API && dotnet ef database update && dotnet run
cd ../E-Commerce.MVC && dotnet run
```

> Requires **.NET 9.0 SDK**, **SQL Server**, and updating connection strings in `appsettings.json`.

---

## Key Implementation Details

### Authentication Flow

```
Customer Registration/Login
        │
        ▼
MVC Controller ────► API Accounts Controller
        │                      │
        │              JWT Token Generated
        │              (Claims: Name, Email, Role)
        │                      │
        │◄─────────────────────┘
        │
        ▼
Token stored in Session
        │
        ▼
Subsequent API calls include
Authorization: Bearer {token}
```

### Order Processing with Stock Management

```
Place Order
    │
    ├──► Validate stock availability
    │
    ├──► Create order record
    │
    ├──► Create order items
    │
    ├──► Decrease product stock quantities
    │
    └──► Return order confirmation


Cancel Order
    │
    ├──► Set order status to Cancelled
    │
    └──► Restore product stock quantities
        (revert the original quantities)
```

### Service Layer Pattern

Every entity has a dedicated service with full CRUD operations, business validation, and DTO mapping:

```csharp
// Interface
public interface IProductService
{
    IEnumerable<ProductDto> GetAllProducts();
    ProductDto GetProductById(int id);
    ProductDto AddProduct(CreateProductDto dto);
    ProductDto UpdateProduct(UpdateProductDto dto);
    void DeleteProduct(int id);
}

// Implementation uses Unit of Work to access repositories
public class ProductService : IProductService
{
    private readonly UOW _uow;
    // Constructor injection
    // Business logic + validation
    // DTO mapping via private helper methods
}
```

---

## Future Improvements

- Implement password hashing using BCrypt or ASP.NET Core Identity
- Add `[Authorize]` attributes to protect API endpoints
- Implement async/await patterns for database operations
- Add pagination and filtering on list endpoints
- Store images in cloud storage (Azure Blob / AWS S3) instead of database
- Move sensitive configuration to environment variables or Azure Key Vault
- Add payment gateway integration (Stripe / PayPal)
- Implement email notifications for order status updates
- Add unit and integration tests
- Implement global exception handling middleware
- Add logging with Serilog or structured logging
- Implement rate limiting on API endpoints

---

## Author

**Aziz Nader Aziz Zaki**
C# .NET Backend Developer | ASP.NET Core Engineer

- GitHub: [github.com/AzizNader1](https://github.com/AzizNader1)
- LinkedIn: [linkedin.com/in/aziznader](https://linkedin.com/in/aziznader/)
- Email: aziznader555@gmail.com
