FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RealEstate.Web/RealEstate.Web.csproj", "RealEstate.Web/"]
RUN dotnet restore "RealEstate.Web/RealEstate.Web.csproj"
COPY . .
WORKDIR "/src/RealEstate.Web"
RUN dotnet build "RealEstate.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RealEstate.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealEstate.Web.dll"]
