FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Dres.TestWebApp/Dres.TestWebApp.csproj", "Dres.TestWebApp/"]
RUN dotnet restore "Dres.TestWebApp/Dres.TestWebApp.csproj"
COPY . .
WORKDIR "/src/Dres.TestWebApp"
RUN dotnet build "Dres.TestWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dres.TestWebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dres.TestWebApp.dll"]
