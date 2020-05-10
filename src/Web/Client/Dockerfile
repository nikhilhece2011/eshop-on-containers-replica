FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS publishcode
WORKDIR /publishcode
COPY Client.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -o ./out -c Release


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=publishcode /publishcode/out .
EXPOSE 80
ENTRYPOINT ["dotnet","Client.dll"]