FROM microsoft/dotnet:2.1-sdk-alpine AS build

WORKDIR /app

# copy csproj and restore as distinct layers
COPY dotnet-web-info.csproj /app/
RUN dotnet restore

# copy everything else and build app
COPY * /app/
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "dotnet-web-info.dll"]