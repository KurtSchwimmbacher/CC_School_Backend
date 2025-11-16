FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project file first for better layer caching
COPY CodeCloudSchool/*.csproj ./CodeCloudSchool/
# Restore dependencies
WORKDIR /app/CodeCloudSchool
RUN dotnet restore

# Copy all source files
COPY CodeCloudSchool/ ./
# Build and publish
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port (Render will set PORT env var)
EXPOSE 80
ENTRYPOINT ["dotnet", "Code_CloudSchool.dll"]
