FROM docker.totvs.io/tnf/build/dotnet_runtime:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM docker.totvs.io/tnf/build/dotnet_sdk_and_node:2.2-sdk AS build
WORKDIR /src
COPY NuGet.Config .
COPY ["ProdutoXyz.Web/ProdutoXyz.Web.csproj", "ProdutoXyz.Web/"]
COPY ["ProdutoXyz.Infra.SqLite/ProdutoXyz.Infra.SqLite.csproj", "ProdutoXyz.Infra.SqLite/"]
COPY ["ProdutoXyz.Infra.Postgres/ProdutoXyz.Infra.Postgres.csproj", "ProdutoXyz.Infra.Postgres/"]
COPY ["ProdutoXyz.Infra/ProdutoXyz.Infra.csproj", "ProdutoXyz.Infra/"]
COPY ["ProdutoXyz.Domain/ProdutoXyz.Domain.csproj", "ProdutoXyz.Domain/"]
COPY ["ProdutoXyz.Dto/ProdutoXyz.Dto.csproj", "ProdutoXyz.Dto/"]
COPY ["ProdutoXyz.Application/ProdutoXyz.Application.csproj", "ProdutoXyz.Application/"]
RUN dotnet restore "ProdutoXyz.Web/ProdutoXyz.Web.csproj"
COPY . .
WORKDIR "/src/ProdutoXyz.Web"
RUN dotnet build "ProdutoXyz.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProdutoXyz.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProdutoXyz.Web.dll"]
