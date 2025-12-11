FROM node:20.16-slim AS build
USER node
WORKDIR /home/node/
RUN npm config set registry https://npm.totvs.io
RUN npm config set @fluig-tools-lib:registry http://nexus.fluig.com/repository/npm-group/
RUN npm config set @fluig-tools-component:registry http://nexus.fluig.com/repository/npm-group/
COPY *.json .
RUN npm ci --force
COPY . .
RUN npm run build

FROM us-central1-docker.pkg.dev/totvsapps-services/devops-foundation/sonar-scanner-cli:5.0 AS sonar
COPY --from=build /home/node/dist/portal-service-ui/browser/ .
COPY sonar-project.properties .
RUN sonarScanner

FROM us-central1-docker.pkg.dev/totvsapps-services/devops-foundation/front:nginx-1.25-alpine-1
COPY --from=build --chown=nonroot /home/node/dist/portal-service-ui/browser /sources/
