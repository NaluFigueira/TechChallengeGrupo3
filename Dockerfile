FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS build
WORKDIR /src
COPY ["./PosTech.TechChallenge.Contacts.Api/PosTech.TechChallenge.Contacts.Api.csproj", "./PosTech.TechChallenge.Contacts.Api/"]
COPY ["./PosTech.TechChallenge.Contacts.Application/PosTech.TechChallenge.Contacts.Application.csproj", "./PosTech.TechChallenge.Contacts.Application/"]
COPY ["./PosTech.TechChallenge.Contacts.Domain/PosTech.TechChallenge.Contacts.Domain.csproj", "./PosTech.TechChallenge.Contacts.Domain/"]
COPY ["./PosTech.TechChallenge.Contacts.Infra/PosTech.TechChallenge.Contacts.Infra.csproj", "./PosTech.TechChallenge.Contacts.Infra/"]
RUN dotnet restore "./PosTech.TechChallenge.Contacts.Api/PosTech.TechChallenge.Contacts.Api.csproj"
COPY . .
RUN dotnet build "./PosTech.TechChallenge.Contacts.Api/PosTech.TechChallenge.Contacts.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS migration
WORKDIR /src
COPY . .
RUN dotnet restore "./PosTech.TechChallenge.Contacts.Infra/PosTech.TechChallenge.Contacts.Infra.csproj"
COPY . .
WORKDIR "/src/PosTech.TechChallenge.Contacts.Infra"
RUN dotnet build "PosTech.TechChallenge.Contacts.Infra.csproj" -c Release -o /app/migration

FROM build AS publish
RUN dotnet publish "./PosTech.TechChallenge.Contacts.Api/PosTech.TechChallenge.Contacts.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /migration
COPY --from=migration /app/migration .
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PosTech.TechChallenge.Contacts.Api.dll"]