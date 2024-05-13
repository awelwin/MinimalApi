FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /Alex.MinimalApi.Service

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /Alex.MinimalApi.Service
COPY --from=build-env /Alex.MinimalApi.Service/out .
ENTRYPOINT ["dotnet", "Alex.MinimalApi.Service.dll"]