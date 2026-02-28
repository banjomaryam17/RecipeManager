# Recipe Manager API

A RESTful API built with ASP.NET Core Web API for managing recipes and ingredients.

## Technologies Used
- ASP.NET Core Web API (.NET 10)
- Entity Framework Core
- SQLite
- JWT Authentication
- BCrypt.Net for password hashing

## API Endpoints

### Auth
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login and receive a JWT token

### Recipes (requires JWT token)
- `GET /api/recipes` - Get all recipes
- `GET /api/recipes/{id}` - Get a recipe by ID
- `POST /api/recipes` - Create a new recipe
- `PUT /api/recipes/{id}` - Update a recipe
- `DELETE /api/recipes/{id}` - Delete a recipe

### Ingredients (public)
- `GET /api/ingredients` - Get all ingredients
- `GET /api/ingredients/{id}` - Get an ingredient by ID
- `POST /api/ingredients` - Create a new ingredient
- `DELETE /api/ingredients/{id}` - Delete an ingredient

## Authentication
This API uses JWT Bearer token authentication. To access protected endpoints:
1. Register a user via `POST /api/auth/register`
2. Login via `POST /api/auth/login` to receive a token
3. Include the token in the Authorization header: `Bearer <your_token>`

## Test Data
The database is seeded with:
- 1 admin user
- 5 ingredients (Flour, Eggs, Milk, Butter, Sugar)
- 3 recipes (Pancakes, Omelette, Crepes)

## Running Tests
```
dotnet test
```
