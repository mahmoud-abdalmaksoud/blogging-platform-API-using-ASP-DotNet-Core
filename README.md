# blogging-platform-API-using-ASP-DotNet-Core
Developed a comprehensive .NET API for managing posts and user authentication. The project involved creating several endpoints for CRUD operations on posts and users, along with authentication mechanism.
- **Tools & Technologies Used**:
    - ASP.NET Core
    - Entity Framework Core
    - AutoMapper
    - JWT (JSON Web Tokens)
    - Microsoft SQL Server
    - LINQ (Language Integrated Query)
    - Dependency Injection

**Detailed Description**:

- **PostController**:
    - Implemented endpoints to handle CRUD operations on posts.
    - Used Entity Framework Core for database interactions.
    - Implemented authentication using JWT to secure endpoints.
    - Developed search functionality to filter posts based on title and content.
    - Ensured asynchronous operations for better performance.
- **AuthController**:
    - Created endpoints for user registration, login, and password reset.
    - Implemented password hashing using cryptographic techniques for security.
    - Generated JWT tokens for user authentication and authorization.
    - Used AutoMapper to streamline object-to-object mapping.
- **UserController**:
    - Developed endpoints for managing user data including retrieval, addition, editing, and deletion of users.
    - Utilized AutoMapper for efficient DTO to entity conversion.
    - Ensured proper error handling and validation for all endpoints.

**Outcome**:

- Successfully developed a robust API that handles various aspects of a blogging platform.
- Improved the understanding of .NET Core and related technologies.
- Demonstrated the ability to implement secure authentication and authorization mechanisms.
- Gained practical experience in developing and managing RESTful APIs.
- **Tools & Technologies Used**:
    - ASP.NET Core
    - Entity Framework Core
    - AutoMapper
    - JWT (JSON Web Tokens)
    - Microsoft SQL Server
    - LINQ (Language Integrated Query)
    - Dependency Injection

**Detailed Description**:

- **PostController**:
    - Implemented endpoints to handle CRUD operations on posts.
    - Used Entity Framework Core for database interactions.
    - Implemented authentication using JWT to secure endpoints.
    - Developed search functionality to filter posts based on title and content.
    - Ensured asynchronous operations for better performance.
- **AuthController**:
    - Created endpoints for user registration, login, and password reset.
    - Implemented password hashing using cryptographic techniques for security.
    - Generated JWT tokens for user authentication and authorization.
    - Used AutoMapper to streamline object-to-object mapping.
- **UserController**:
    - Developed endpoints for managing user data including retrieval, addition, editing, and deletion of users.
    - Utilized AutoMapper for efficient DTO to entity conversion.
    - Ensured proper error handling and validation for all endpoints.

**Outcome**:

- Successfully developed a robust API that handles various aspects of a blogging platform.
- Improved the understanding of .NET Core and related technologies.
- Demonstrated the ability to implement secure authentication and authorization mechanisms.
- Gained practical experience in developing and managing RESTful APIs.
## API Endpoints

- **GET /Post/Posts**: Get all posts
- **GET /Post/PostSingle/{postId}**: Get a specific post by ID
- **GET /Post/PostsByUser/{userId}**: Get posts by a specific user
- **GET /Post/MyPosts**: Get posts created by the authenticated user
- **GET /Post/PostsBySearch/{searchParam}**: Search for posts by title or content
- **POST /Post/Post**: Add a new post
- **PUT /Post/Post**: Edit an existing post
- **DELETE /Post/delete-Post/{postId}**: Delete a post
- **POST /Auth/Register**: Register a new user
- **POST /Auth/Login**: Login an existing user
- **PUT /Auth/Reset-Password**: Reset user password
- **GET /Auth/RefreshToken**: Refresh authentication token
