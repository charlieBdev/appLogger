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
# appLogger

`appLogger` is an ASP.NET Core MVC application to view logs from multiple PostgreSQL databases for different applications and environments. It supports dynamic database selection, light/dark theming, and is configured to work with local development databases using PostgreSQL’s trust authentication.

## Features

- Select **Application** and **Environment** from dropdowns to view logs.
- Multiple PostgreSQL databases configurable in `appsettings.Development.json` or user secrets.
- Light/Dark theme using CSS variables.
- Uses Entity Framework Core to connect dynamically to selected databases.
- Seedable logging table for testing.
- Dropdowns refresh dynamically based on selected app.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- VS Code or another IDE (optional)
- User Secrets (built into .NET SDK)

## Getting Started

1. **Clone the repository** and enter the folder:

    ```bash
    git clone <repository-url>
    cd appLogger
    ```

2. **Start PostgreSQL** in the CLI and ensure trust authentication is enabled for local development (no username/password). Create local databases with generic names:

    ```sql
    CREATE DATABASE app1_dev;
    CREATE DATABASE app1_prod;
    CREATE DATABASE app2_test;
    CREATE DATABASE app2_prod;
    CREATE DATABASE app3_staging;
    CREATE DATABASE app3_prod;
    ```

3. **Create `appsettings.Development.json`** in the project root with the following content:

    ```json
    {
      "LoggingDatabases": {
        "App1": {
          "Dev": "Host=localhost;Database=app1_dev",
          "Prod": "Host=localhost;Database=app1_prod"
        },
        "App2": {
          "Test": "Host=localhost;Database=app2_test",
          "Prod": "Host=localhost;Database=app2_prod"
        },
        "App3": {
          "Staging": "Host=localhost;Database=app3_staging",
          "Prod": "Host=localhost;Database=app3_prod"
        }
      }
    }
    ```

    > **Note:** No username or password is required for local trust authentication. For production, use User Secrets to hide credentials:

    ```bash
    dotnet user-secrets init
    dotnet user-secrets set "LoggingDatabases:App1:Prod" "Host=prod-server;Database=app1_prod;Username=prodUser;Password=prodPassword"
    ```

4. **Seed the logging table** in each database. Connect to each database in the CLI, e.g., `\c app1_dev`, then run:

    ```sql
    CREATE TABLE logging (
        id SERIAL PRIMARY KEY,
        timestamp TIMESTAMP NOT NULL,
        level TEXT NOT NULL,
        message TEXT NOT NULL
    );

    INSERT INTO logging (timestamp, level, message) VALUES
        (NOW(), 'INFO', 'Application started'),
        (NOW(), 'ERROR', 'Something went wrong'),
        (NOW(), 'WARNING', 'This is a warning');
    ```

    Repeat for each database using different levels and messages if desired.

5. **Run the application** in development mode:

    ```bash
    dotnet watch run --environment Development
    ```

    Open a browser at:

    ```
    http://localhost:{LocalPort}/Logs
    ```

    Select the **App** and **Environment**, then click **Submit** to view logs. On first load, no logs are displayed until a selection is made.

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

Use the light-dark(light, dark) helper to apply colors dynamically.

## Development Notes

LoggingContext uses EF Core to connect dynamically to the selected database.

Controller passes App and Environment lists to the view.

User secrets keep development/production connection strings safe.

For production, use secure credentials; do not rely on trust authentication.

## Git Ignore

Add the following to `.gitignore`:

```
bin/
obj/
*.user
appsettings.Development.json
```

## License

MIT License
