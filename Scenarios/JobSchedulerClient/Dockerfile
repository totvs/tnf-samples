FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
COPY NuGet.Config .
WORKDIR /src
COPY ["src/JobSchedulerClient.Web/JobSchedulerClient.Web.csproj", "src/JobSchedulerClient.Web/"]
RUN dotnet restore "src/JobSchedulerClient.Web/JobSchedulerClient.Web.csproj"
COPY . .
WORKDIR "/src/src/JobSchedulerClient.Web"
RUN dotnet build "JobSchedulerClient.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "JobSchedulerClient.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JobSchedulerClient.Web.dll"]