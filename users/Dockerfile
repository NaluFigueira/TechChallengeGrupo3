FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS build
WORKDIR /src
COPY ["./PosTech.TechChallenge.Users.Api/PosTech.TechChallenge.Users.Api.csproj", "./PosTech.TechChallenge.Users.Api/"]
COPY ["./PosTech.TechChallenge.Users.Application/PosTech.TechChallenge.Users.Application.csproj", "./PosTech.TechChallenge.Users.Application/"]
COPY ["./PosTech.TechChallenge.Users.Domain/PosTech.TechChallenge.Users.Domain.csproj", "./PosTech.TechChallenge.Users.Domain/"]
COPY ["./PosTech.TechChallenge.Users.Infra/PosTech.TechChallenge.Users.Infra.csproj", "./PosTech.TechChallenge.Users.Infra/"]
RUN dotnet restore "./PosTech.TechChallenge.Users.Api/PosTech.TechChallenge.Users.Api.csproj"
COPY . .
RUN dotnet build "./PosTech.TechChallenge.Users.Api/PosTech.TechChallenge.Users.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS migration
WORKDIR /src
COPY . .
RUN dotnet restore "./PosTech.TechChallenge.Users.Infra/PosTech.TechChallenge.Users.Infra.csproj"
COPY . .
WORKDIR "/src/PosTech.TechChallenge.Users.Infra"
RUN dotnet build "PosTech.TechChallenge.Users.Infra.csproj" -c Release -o /app/migration

FROM build AS publish
RUN dotnet publish "./PosTech.TechChallenge.Users.Api/PosTech.TechChallenge.Users.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /migration
COPY --from=migration /app/migration .
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PosTech.TechChallenge.Users.Api.dll"]