#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 64169

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BoutiquesFinder/BoutiquesFinder.WebApi.csproj", "BoutiquesFinder/"]
COPY ["BoutiquesFinder.Application/BoutiquesFinder.Application.csproj", "BoutiquesFinder.Application/"]
COPY ["BoutiquesFinder.Domain/BoutiquesFinder.Domain.csproj", "BoutiquesFinder.Domain/"]
RUN dotnet restore "BoutiquesFinder/BoutiquesFinder.WebApi.csproj"
COPY . .
WORKDIR "/src/BoutiquesFinder"
RUN dotnet build "BoutiquesFinder.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoutiquesFinder.WebApi.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoutiquesFinder.WebApi.dll"]