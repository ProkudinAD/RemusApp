FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5119

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Services/ContentService/Content.WebApi/Content.WebApi.csproj", "Services/ContentService/Content.WebApi/"]
COPY ["Services/ContentService/Content.Application/Content.Application.csproj", "Services/ContentService/Content.Application/"]
COPY ["Services/ContentService/Content.Domain/Content.Domain.csproj", "Services/ContentService/Content.Domain/"]
COPY ["Services/ContentService/Content.Infrastructure/Content.Infrastructure.csproj", "Services/ContentService/Content.Infrastructure/"]
COPY ["Services/ContentService/Content.Contracts/Content.Contracts.csproj", "Services/ContentService/Content.Contracts/"]
RUN dotnet restore "Services/ContentService/Content.WebApi/Content.WebApi.csproj"
COPY . .
WORKDIR "/src/Services/ContentService/Content.WebApi"
RUN dotnet build "Content.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Content.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Content.WebApi.dll"]