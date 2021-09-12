FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
ARG BUILDCONFIG=RELEASE
ARG VERSION=0.0.1

COPY . .

# RUN dotnet restore "src/LingoBank.API/LingoBank.API.csproj"
# RUN dotnet test "src/LingoBank.API.UnitTests/LingoBank.API.UnitTests.csproj"

# FROM build AS publish
# RUN dotnet publish "src/LingoBank.API/LingoBank.API.csproj" -c Release -o /app

FROM node:12 AS webbuild
WORKDIR /src
COPY --from=build /src .
RUN mkdir -p wwwroot

RUN ls -l

WORKDIR /src/src/LingoBank.WebApp
# COPY package.json .
RUN npm install


RUN ls -l

RUN npm run build


RUN cp -R dist/* /src/wwwroot

WORKDIR /src
RUN ls -l

FROM base AS final
WORKDIR /app
COPY --from=webbuild /src/wwwroot ./wwwroot
COPY --from=publish /app .
RUN ls -l

ENTRYPOINT ["dotnet", "LingoBank.API.dll"]
