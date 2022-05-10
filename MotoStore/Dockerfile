FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MotoStore/MotoStore.csproj", "MotoStore/"]
COPY ["CommonData/CommonData.csproj", "CommonData/"]
RUN dotnet restore "MotoStore/MotoStore.csproj"
COPY . .
WORKDIR "/src/MotoStore"
RUN dotnet build "MotoStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MotoStore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MotoStore.dll"]
