FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Stacks_rework.csproj", "."]
RUN dotnet restore "./Stacks_rework.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Stacks_rework.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stacks_rework.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stacks_rework.dll"]