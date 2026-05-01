# 🛒 E-Commerce Management API — Modular Monolith

A clean, production-ready **ASP.NET Core 8** REST API structured as a **Modular Monolith** — one deployable, three self-contained domain modules.

---

## 🏗️ Project Structure

```
ECommerceAPI/
│
├── Modules/
│   ├── Auth/                        ← Auth module
│   │   ├── AuthController.cs
│   │   ├── AuthService.cs
│   │   ├── AuthModule.cs            ← registers its own services
│   │   ├── DTOs/
│   │   │   └── AuthDtos.cs
│   │   └── Interfaces/
│   │       └── IAuthService.cs
│   │
│   ├── Products/                    ← Products module
│   │   ├── ProductsController.cs
│   │   ├── ProductService.cs
│   │   ├── ProductRepository.cs
│   │   ├── ProductsModule.cs
│   │   ├── DTOs/
│   │   │   └── ProductDtos.cs
│   │   └── Interfaces/
│   │       └── IProductInterfaces.cs
│   │
│   └── Orders/                      ← Orders module
│       ├── OrdersController.cs
│       ├── OrderService.cs
│       ├── OrderRepository.cs
│       ├── OrdersModule.cs
│       ├── DTOs/
│       │   └── OrderDtos.cs
│       └── Interfaces/
│           └── IOrderInterfaces.cs
│
├── Shared/                          ← No module imports from another module
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   └── DbSeeder.cs
│   ├── Entities/
│   │   ├── ApplicationUser.cs
│   │   ├── Product.cs
│   │   ├── Order.cs
│   │   └── OrderItem.cs
│   ├── Helpers/
│   │   ├── ApiResponse.cs
│   │   ├── JwtTokenHelper.cs
│   │   └── Roles.cs
│   ├── Mappings/
│   │   └── MappingProfile.cs
│   └── Middleware/
│       └── GlobalExceptionMiddleware.cs
│
├── Program.cs                       ← Composition root only
├── appsettings.json
└── ECommerceAPI.csproj
```

---

## Modular Monolith Rules Applied

1. **Each module owns its own Controller, Service, Repository, DTOs, and Interfaces.**
2. **Modules never import from each other directly** — the one cross-module dependency (Orders reading Products) goes through `IProductRepository`, not `ProductService`.
3. **Each module registers itself** via `AddAuthModule()`, `AddProductsModule()`, `AddOrdersModule()`.
4. **`Program.cs` is a composition root only** — it wires modules together, nothing else.
5. **`Shared/` contains only what truly belongs to no single module** — entities, DbContext, middleware, JWT, AutoMapper.

---

## Authentication & Roles

| Role     | Capabilities                                     |
|----------|--------------------------------------------------|
| Admin    | Full product CRUD, view all products             |
| Customer | Browse active products, place & view own orders  |

---

## API Endpoints

### Auth — `/api/auth`
| Method | Endpoint    | Access | Description            |
|--------|-------------|--------|------------------------|
| POST   | /register   | Public | Register as customer   |
| POST   | /login      | Public | Login, receive JWT     |

### Products — `/api/products`
| Method | Endpoint    | Role     | Description                    |
|--------|-------------|----------|--------------------------------|
| GET    | /           | Customer | Browse active in-stock products|
| GET    | /all        | Admin    | All products (incl. inactive)  |
| GET    | /{id}       | Any auth | Get product by ID              |
| POST   | /           | Admin    | Create product                 |
| PUT    | /{id}       | Admin    | Update product                 |
| DELETE | /{id}       | Admin    | Delete product                 |

### Orders — `/api/orders`
| Method | Endpoint    | Role     | Description                  |
|--------|-------------|----------|------------------------------|
| POST   | /           | Customer | Place an order               |
| GET    | /my         | Customer | View all my orders           |
| GET    | /{id}       | Customer | View a specific order        |

---

Seeded admin account:
- Email: `admin@ecommerce.com`
- Password: `Admin@123456`
