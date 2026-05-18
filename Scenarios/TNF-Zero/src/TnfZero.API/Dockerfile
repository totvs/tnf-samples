FROM us-central1-docker.pkg.dev/totvsapps-services/devops-foundation/tnf:runtime-debian-8.0-latest
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY . .
USER root
RUN hardening-script
USER nonroot
ENTRYPOINT ["dotnet", "TnfZero.API.dll"]