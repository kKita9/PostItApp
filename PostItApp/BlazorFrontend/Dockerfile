FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .

WORKDIR /src/BlazorFrontend

RUN dotnet restore BlazorFrontend.csproj

RUN dotnet build BlazorFrontend.csproj -c Release -o /app/build

FROM build AS publish
WORKDIR /src/BlazorFrontend
RUN dotnet publish BlazorFrontend.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorFrontend.dll"]
