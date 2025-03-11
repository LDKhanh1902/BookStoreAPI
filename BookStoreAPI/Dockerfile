# Stage 1: Runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Stage 2: Build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project file and restore dependencies
COPY ["BookStoreAPI/BookStoreAPI.csproj", "BookStoreAPI/"]
RUN dotnet restore "BookStoreAPI/BookStoreAPI.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/BookStoreAPI"

# Build application
RUN dotnet build "BookStoreAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish application
FROM build AS publish
RUN dotnet publish "BookStoreAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Stage 4: Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Run the application
ENTRYPOINT ["dotnet", "BookStoreAPI.dll"]
