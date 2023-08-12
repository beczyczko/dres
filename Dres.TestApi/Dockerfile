FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Dres.TestApi/Dres.TestApi.csproj", "Dres.TestApi/"]
RUN dotnet restore "Dres.TestApi/Dres.TestApi.csproj"
COPY . .
WORKDIR "/src/Dres.TestApi"
RUN dotnet build "Dres.TestApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dres.TestApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dres.TestApi.dll"]
