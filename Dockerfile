FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src/src
RUN dotnet restore Explorer.API/Explorer.API.csproj
RUN dotnet build Explorer.API/Explorer.API.csproj -c Release

FROM build as publish
RUN dotnet publish Explorer.API/Explorer.API.csproj -c Release -o /app/publish

ENV ASPNETCORE_URLS=http://+:80
FROM base AS final
COPY --from=publish /app .

#WORKDIR /app
#COPY entrypoint.sh .
#RUN chmod +x entrypoint.sh
#COPY --from=publish /app/publish .
#ENTRYPOINT ["./entrypoint.sh"]

WORKDIR /app/publish
CMD ["dotnet", "Explorer.API.dll"]
