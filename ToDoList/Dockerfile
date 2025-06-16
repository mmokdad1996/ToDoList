# Use official ASP.NET runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app



# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy only project file first to improve caching
COPY ["ToDoList.csproj", "./"]
RUN dotnet restore "ToDoList.csproj"

# Copy rest of the source code
COPY . ./
RUN dotnet build -c Release -o /app/build

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Ensure entry point runs correctly
EXPOSE 80
CMD ["dotnet", "ToDoList.dll"]
