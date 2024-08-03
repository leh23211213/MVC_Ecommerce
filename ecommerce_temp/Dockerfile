#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 433

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ecommerce_temp/ecommerce_temp.csproj", "ecommerce_temp/"]
RUN dotnet restore "ecommerce_temp/ecommerce_temp.csproj"
COPY . .
WORKDIR "/src/ecommerce_temp"
RUN dotnet build "./ecommerce_temp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ecommerce_temp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ecommerce_temp.dll"]