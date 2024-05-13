FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /Alex.MinimalApi.Service


# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Debug -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /Alex.MinimalApi.Service
EXPOSE 4040
ENV ASPNETCORE_URLS=http://*:4040
COPY --from=build-env /Alex.MinimalApi.Service/out .
ENTRYPOINT ["dotnet", "Alex.MinimalApi.Service.dll", "--environment=DevelopmentDocker"]