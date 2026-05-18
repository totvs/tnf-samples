FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY nuget.config .
COPY Directory.Packages.props .
COPY ["src/JobSchedulerClient.Web/JobSchedulerClient.Web.csproj", "src/JobSchedulerClient.Web/"]
RUN dotnet restore "src/JobSchedulerClient.Web/JobSchedulerClient.Web.csproj"
COPY . .
WORKDIR "/src/src/JobSchedulerClient.Web"
RUN dotnet build "JobSchedulerClient.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobSchedulerClient.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobSchedulerClient.Web.dll"]
