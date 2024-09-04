FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS build
WORKDIR /src
COPY ["./PosTech.TechChallenge.Contacts.Query.Api/PosTech.TechChallenge.Contacts.Query.Api.csproj", "./PosTech.TechChallenge.Contacts.Query.Api/"]
COPY ["./PosTech.TechChallenge.Contacts.Query.Application/PosTech.TechChallenge.Contacts.Query.Application.csproj", "./PosTech.TechChallenge.Contacts.Query.Application/"]
COPY ["./PosTech.TechChallenge.Contacts.Query.Domain/PosTech.TechChallenge.Contacts.Query.Domain.csproj", "./PosTech.TechChallenge.Contacts.Query.Domain/"]
COPY ["./PosTech.TechChallenge.Contacts.Query.Infra/PosTech.TechChallenge.Contacts.Query.Infra.csproj", "./PosTech.TechChallenge.Contacts.Query.Infra/"]
RUN dotnet restore "./PosTech.TechChallenge.Contacts.Query.Api/PosTech.TechChallenge.Contacts.Query.Api.csproj"
COPY . .
RUN dotnet build "./PosTech.TechChallenge.Contacts.Query.Api/PosTech.TechChallenge.Contacts.Query.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS migration
WORKDIR /src
COPY . .
RUN dotnet restore "./PosTech.TechChallenge.Contacts.Query.Infra/PosTech.TechChallenge.Contacts.Query.Infra.csproj"
COPY . .
WORKDIR "/src/PosTech.TechChallenge.Contacts.Query.Infra"
RUN dotnet build "PosTech.TechChallenge.Contacts.Query.Infra.csproj" -c Release -o /app/migration

FROM build AS publish
RUN dotnet publish "./PosTech.TechChallenge.Contacts.Query.Api/PosTech.TechChallenge.Contacts.Query.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /migration
COPY --from=migration /app/migration .
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PosTech.TechChallenge.Contacts.Query.Api.dll"]