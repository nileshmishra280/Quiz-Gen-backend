# Build ke liye base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Current folder se .csproj copy karo
COPY *.csproj ./
RUN dotnet restore

# Current folder se saara code copy karo
COPY . ./
RUN dotnet publish -c Release -o out

# Run ke liye image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Quizee.Api.dll"]