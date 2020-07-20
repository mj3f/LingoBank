FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
ARG BUILDCONFIG=RELEASE
ARG VERSION=0.0.1

COPY . .

RUN dotnet restore "src/LingoBank.API/LingoBank.API.csproj"

FROM build AS publish
RUN dotnet publish "src/LingoBank.API/LingoBank.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "LingoBank.API.dll"]
