FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS build
WORKDIR /src
COPY ["./PosTech.TechChallenge.Contacts.Command.Api/PosTech.TechChallenge.Contacts.Command.Api.csproj", "./PosTech.TechChallenge.Contacts.Command.Api/"]
COPY ["./PosTech.TechChallenge.Contacts.Command.Application/PosTech.TechChallenge.Contacts.Command.Application.csproj", "./PosTech.TechChallenge.Contacts.Command.Application/"]
COPY ["./PosTech.TechChallenge.Contacts.Command.Domain/PosTech.TechChallenge.Contacts.Command.Domain.csproj", "./PosTech.TechChallenge.Contacts.Command.Domain/"]
COPY ["./PosTech.TechChallenge.Contacts.Command.Infra/PosTech.TechChallenge.Contacts.Command.Infra.csproj", "./PosTech.TechChallenge.Contacts.Command.Infra/"]
RUN dotnet restore "./PosTech.TechChallenge.Contacts.Command.Api/PosTech.TechChallenge.Contacts.Command.Api.csproj"
COPY . .
RUN dotnet build "./PosTech.TechChallenge.Contacts.Command.Api/PosTech.TechChallenge.Contacts.Command.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS migration
WORKDIR /src
COPY . .
RUN dotnet restore "./PosTech.TechChallenge.Contacts.Command.Infra/PosTech.TechChallenge.Contacts.Command.Infra.csproj"
COPY . .
WORKDIR "/src/PosTech.TechChallenge.Contacts.Command.Infra"
RUN dotnet build "PosTech.TechChallenge.Contacts.Command.Infra.csproj" -c Release -o /app/migration

FROM build AS publish
RUN dotnet publish "./PosTech.TechChallenge.Contacts.Command.Api/PosTech.TechChallenge.Contacts.Command.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /migration
COPY --from=migration /app/migration .
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PosTech.TechChallenge.Contacts.Command.Api.dll"]