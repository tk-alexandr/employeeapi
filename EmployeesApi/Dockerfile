FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EmployeesApi/EmployeesApi.csproj", "EmployeesApi/"]
COPY ["Employees.DataStoreMigrations/Employees.DataStoreMigrations.csproj", "Employees.DataStoreMigrations/"]
COPY ["Employees.DataStore/Employees.DataStore.csproj", "Employees.DataStore/"]
RUN dotnet restore "EmployeesApi/EmployeesApi.csproj"
COPY . .
WORKDIR "/src/EmployeesApi"
RUN dotnet build "EmployeesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeesApi.dll"]