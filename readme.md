# appLogger

`appLogger` is an ASP.NET Core MVC application to **view logs from multiple PostgreSQL databases** for different applications and environments. It supports **dynamic database selection**, **light/dark theming**, and is configured to work with **local development databases** using PostgreSQL’s trust authentication.

---

## Features

* Select **Application** and **Environment** from dropdowns to view logs.
* Multiple PostgreSQL databases configurable in `appsettings` or secrets.
* **Light/Dark theme** with a simple `light-dark()` helper.
* Uses **Entity Framework Core** to connect dynamically to selected databases.
* Seedable logging table for testing.
* Dropdowns refresh dynamically based on selected app.

---

## Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [PostgreSQL](https://www.postgresql.org/download/)
* [VS Code](https://code.visualstudio.com/) or another IDE

---

## Setup

### 1. Clone the repository

```bash
git clone <repository-url>
cd appLogger
```

### 2. Configure PostgreSQL

Start PostgreSQL in the CLI before creating databases. Then create local databases for development (use general names):

```sql
CREATE DATABASE app1_dev;
CREATE DATABASE app1_prod;
CREATE DATABASE app2_test;
CREATE DATABASE app2_prod;
CREATE DATABASE app3_staging;
CREATE DATABASE app3_prod;
```

Ensure **trust authentication** is enabled for local development so no username/password is required.

---

### 3. Configure Connection Strings

Use **User Secrets** for local development:

```bash
dotnet user-secrets init
dotnet user-secrets set "LoggingDatabases:App1:Dev" "Host=localhost;Database=app1_dev"
dotnet user-secrets set "LoggingDatabases:App1:Prod" "Host=localhost;Database=app1_prod"
dotnet user-secrets set "LoggingDatabases:App2:Test" "Host=localhost;Database=app2_test"
dotnet user-secrets set "LoggingDatabases:App2:Prod" "Host=localhost;Database=app2_prod"
dotnet user-secrets set "LoggingDatabases:App3:Staging" "Host=localhost;Database=app3_staging"
dotnet user-secrets set "LoggingDatabases:App3:Prod" "Host=localhost;Database=app3_prod"
```

> **Note:** No username or password is required for local trust authentication.

---

### 4. Seed the Logging Table

Before seeding, connect to each database in the CLI one at a time. Then create the `logging` table and insert sample data:

```sql
-- Connect to each database first, e.g., \c app1_dev
CREATE TABLE logging (
    id SERIAL PRIMARY KEY,
    timestamp TIMESTAMP NOT NULL,
    level TEXT NOT NULL,
    message TEXT NOT NULL
);

INSERT INTO logging (timestamp, level, message)
VALUES
    (NOW(), 'INFO', 'Application started'),
    (NOW(), 'ERROR', 'Something went wrong'),
    (NOW(), 'WARNING', 'This is a warning');
```

Repeat for each database using different levels and messages.

---

### 5. Run the Application

```bash
dotnet run
```

Open a browser at:

```
http://localhost:{LocalPort}/Logs
```

* Select the **App** and **Environment**.
* Click **Submit** to view logs.

---

## Light/Dark Theme

CSS variables define colors for light and dark themes:

```css
:root {
  --light-bg: #ffffff;
  --light-color: #121212;

  --dark-bg: #121212;
  --dark-color: #e0e0e0;
}
```

Use the `light-dark(light, dark)` helper to apply colors dynamically.

---

## Development Notes

* **`LoggingContext`** uses EF Core to connect dynamically to the selected database.
* Controller passes **App and Environment lists** to the view.
* **User secrets** keep development connection strings safe.
* For production, use **secure credentials** and do not rely on trust authentication.

---

## Git Ignore

Add the following to `.gitignore`:

```
bin/
obj/
*.user
appsettings.Development.json
```

---

## License

MIT License