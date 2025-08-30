# Etapa 1: Build da Aplicação
# imagem SDK do .NET 9 para compilar o código fonte
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# 1. Copiando o arquivo da solução (.sln)
COPY FastFood.sln ./

# 2. Copiando arquivos .csproj necessários para a restauração
COPY FastFood.API/FastFood.API.csproj ./FastFood.API/
COPY FastFood.DataSource/FastFood.DataSource.csproj ./FastFood.DataSource/
COPY FastFood.CoreController/FastFood.CoreController.csproj ./FastFood.CoreController/
COPY FastFood.Gateway/FastFood.Gateway.csproj ./FastFood.Gateway/
COPY FastFood.Application/FastFood.Application.csproj ./FastFood.Application/
COPY FastFood.Domain/FastFood.Domain.csproj ./FastFood.Domain/
COPY FastFood.Infra.Data/FastFood.Infra.Data.csproj ./FastFood.Infra.Data/
COPY FastFood.Infra.ExternalServices/FastFood.Infra.ExternalServices.csproj ./FastFood.Infra.ExternalServices/
COPY FastFood.Infra.IoC/FastFood.Infra.IoC.csproj ./FastFood.Infra.IoC/
COPY FastFood.Tests/FastFood.Tests.csproj ./FastFood.Tests/

# 3. Restaurando as dependências de todos os projetos listados no .sln
RUN dotnet restore "FastFood.sln"

# 4. Copiando todo o código fonte para o diretório de trabalho
COPY . ./

# 5. Publicando o projeto da API
RUN dotnet publish "FastFood.API/FastFood.API.csproj" -c Release -o /app/publish

# Estágio 2: Runtime da Aplicação
# imagem ASP.NET Core Runtime do .NET 9
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copiando os artefatos publicados do estágio de build
COPY --from=build-env /app/publish .

# Definindo variáveis de ambiente
ENV ASPNETCORE_ENVIRONMENT=Development
# Kestrel escutará na porta 8080 dentro do container
ENV ASPNETCORE_URLS=http://+:8080 

# Exponha a porta que a aplicação está escutando dentro do container
EXPOSE 8080

# Ponto de entrada: Comando para iniciar sua aplicação API
ENTRYPOINT ["dotnet", "FastFood.API.dll"]