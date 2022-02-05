FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PersonService/PersonService.csproj", "PersonService/"]
RUN dotnet restore "PersonService/PersonService.csproj"
COPY . .
WORKDIR "/src/PersonService"
RUN dotnet build "PersonService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonService.dll"]
