FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview-jammy AS base
WORKDIR /app

EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-preview-jammy AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*
    
ARG configuration=Release
WORKDIR /src
COPY ["Dres.Catwalk/Dres.Catwalk.csproj", "Dres.Catwalk/"]
RUN dotnet restore "Dres.Catwalk/Dres.Catwalk.csproj"
COPY . .
WORKDIR "/src/Dres.Catwalk"
RUN dotnet build "Dres.Catwalk.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Dres.Catwalk.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dres.Catwalk.dll"]
