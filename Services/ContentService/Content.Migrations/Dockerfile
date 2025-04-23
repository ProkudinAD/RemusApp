FROM mcr.microsoft.com/dotnet/sdk:9.0

WORKDIR /src

COPY ["Services/ContentService/Content.Migrations/*.csproj", "Services/ContentService/Content.Migrations/"]
COPY ["Services/ContentService/Content.Infrastructure/*.csproj", "Services/ContentService/Content.Infrastructure/"]

RUN dotnet restore "Services/ContentService/Content.Migrations/Content.Migrations.csproj"

COPY . .

RUN dotnet build "Services/ContentService/Content.Migrations/Content.Migrations.csproj" -c Release

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "ef", "database", "update", "--project", "Services/ContentService/Content.Migrations/Content.Migrations.csproj"]