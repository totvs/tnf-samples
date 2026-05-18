ARG SONAR_PROJECT_KEY=tnf-zero
ARG SONAR_COVERAGE=tests/coverageResults/coverage.opencover.xml
ARG SONAR_QUALITY_GATE_WAIT=true
ARG SONAR_EXCLUSIONS=\
**/src/TnfZero.EntityFramework*/**,\
**/Program.cs,\
**/Using.cs,\
**/appsettings*.json

FROM us-central1-docker.pkg.dev/totvsapps-services/devops-foundation/tnf:sonar-8.0-latest AS build
COPY ["Directory.Packages.props", "NuGet.Config", "*.sln", "src/*/*.csproj", "./"]
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done
RUN dotnet restore "src/TnfZero.API/TnfZero.API.csproj"
COPY ["src/", "src/"]
COPY ["tests/", "tests/"]
RUN sonarBegin && \
    dotnet build "TnfZero.sln" -c Release -o /app/build

FROM build AS test
RUN testRunner

FROM build AS publish
RUN dotnet publish "src/TnfZero.API/TnfZero.API.csproj" -c Release -o /app/publish

FROM test AS sonar
COPY --from=test /app/build .
RUN sonarScanner

FROM us-central1-docker.pkg.dev/totvsapps-services/devops-foundation/tnf:runtime-debian-8.0-latest AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=publish /app/publish .
USER root
RUN hardening-script
USER nonroot
ENTRYPOINT ["dotnet", "TnfZero.API.dll"]