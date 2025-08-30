# Etapa 1: Build da Aplica��o
# imagem SDK do .NET 9 para compilar o c�digo fonte
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# 1. Copiando o arquivo da solu��o (.sln)
COPY FastFood.sln ./

# 2. Copiando arquivos .csproj necess�rios para a restaura��o
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

# 3. Restaurando as depend�ncias de todos os projetos listados no .sln
RUN dotnet restore "FastFood.sln"

# 4. Copiando todo o c�digo fonte para o diret�rio de trabalho
COPY . ./

# 5. Publicando o projeto da API
RUN dotnet publish "FastFood.API/FastFood.API.csproj" -c Release -o /app/publish

# Est�gio 2: Runtime da Aplica��o
# imagem ASP.NET Core Runtime do .NET 9
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copiando os artefatos publicados do est�gio de build
COPY --from=build-env /app/publish .

# Definindo vari�veis de ambiente
ENV ASPNETCORE_ENVIRONMENT=Development
# Kestrel escutar� na porta 8080 dentro do container
ENV ASPNETCORE_URLS=http://+:8080 

# Exponha a porta que a aplica��o est� escutando dentro do container
EXPOSE 8080

# Ponto de entrada: Comando para iniciar sua aplica��o API
ENTRYPOINT ["dotnet", "FastFood.API.dll"]