# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy the .csproj and restore as distinct layers
COPY . .
RUN dotnet restore

# Copy everything else and build the API
COPY . ./
RUN dotnet publish "./DotNetApi.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

COPY ./appsettings.Development.json ./

# Start the API
ENTRYPOINT ["dotnet", "DotNetApi.dll"]