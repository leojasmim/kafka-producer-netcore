FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["kafka-producer-netcore.csproj", "."]
RUN dotnet restore "./kafka-producer-netcore.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "kafka-producer-netcore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "kafka-producer-netcore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV ASPNETCORE_KafkaConfiguration__HostName=kafka:29092
ENV ASPNETCORE_KafkaConfiguration__TopicName=message.receive

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "kafka-producer-netcore.dll"]
EXPOSE 5165
EXPOSE 9092