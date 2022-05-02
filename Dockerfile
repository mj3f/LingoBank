FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
ARG BUILDCONFIG=RELEASE
ARG VERSION=0.0.1

COPY lingo/. .

# RUN dotnet restore "src/LingoBank.API/LingoBank.API.csproj"
RUN dotnet restore "LingoBank.sln"


FROM build AS publish
RUN dotnet publish "src/LingoBank.API/LingoBank.API.csproj" -c Release -o /app

FROM node:12 AS webbuild
WORKDIR /src
# Copy API build from previous workspace.
COPY --from=build /src .

# Create wwwroot folder which is where the built web project will be housed.
RUN mkdir -p wwwroot

WORKDIR /src/src/LingoBank.WebApp

RUN npm install

RUN npm run build

# Copy ng build files to wwwroot folder.
RUN cp -R dist/* /src/wwwroot

FROM base AS final
WORKDIR /app
COPY --from=webbuild /src/wwwroot ./wwwroot
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "LingoBank.API.dll"]
