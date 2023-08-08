
ARG SONAR_PROJECT_KEY=support-element-provisioning-dap-api
ARG SONAR_COVERAGE=tests/coverageResults/coverage.opencover.xml
ARG SONAR_QUALITY_GATE_WAIT=true
ARG SONAR_EXCLUSIONS=\
Tnf-CarShop.EntityFrameworkCore.Migrator/**,\
Tnf-CarShop.EntityFrameworkCore.PostgreSql/**,\
**/appsettings*.json

FROM us-central1-docker.pkg.dev/totvsapps-services/devops-foundation/tnf:sonar-6.0-latest AS build
ARG SOLUTION_PATH="Tnf-CarShop/Tnf-CarShop.sln"
COPY ["NuGet.Config", "./"]
COPY ["Tnf-CarShop/Tnf.CarShop.sln", "Tnf-CarShop/"]
COPY ["Tnf-CarShop/src/*/*.csproj", "./"]
RUN for file in $(ls *.csproj); do mkdir -p Tnf-CarShop/src/${file%.*}/ && mv $file Tnf-CarShop/src/${file%.*}/; done
COPY ["src/*/*.csproj", "./"]
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done
RUN dotnet restore "Tnf-CarShop/src/Tnf-CarShop.Host/Tnf-CarShop.Host.csproj"
COPY ["Tnf-CarShop/", "Tnf-CarShop/"]
COPY ["src/", "src/"]
RUN dotnet build "Tnf-CarShop/src/Tnf-CarShop.Host/Tnf-CarShop.Host.csproj" -c Release -o /app/build
RUN testRunner

FROM build AS publish
RUN dotnet publish "Tnf-CarShop/src/Tnf-CarShop.Host/Tnf-CarShop.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM us-central1-docker.pkg.dev/totvsapps-services/devops-foundation/tnf:runtime-6.0-latest AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
RUN useradd nonroot -m
USER nonroot
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tnf-CarShop.Host.dll"]

FROM build AS sonar
COPY --from=build /app/build .
RUN sonarScanner

FROM base AS final
