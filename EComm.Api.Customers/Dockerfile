#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EComm.Api.Customers/EComm.Api.Customers.csproj", "EComm.Api.Customers/"]
RUN dotnet restore "EComm.Api.Customers/EComm.Api.Customers.csproj"
COPY . .
WORKDIR "/src/EComm.Api.Customers"
RUN dotnet build "EComm.Api.Customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EComm.Api.Customers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EComm.Api.Customers.dll"]