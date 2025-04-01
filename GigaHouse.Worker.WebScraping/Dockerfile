FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Instalar dependências necessárias, Google Chrome e ChromeDriver
RUN apt-get update && apt-get install -y \
    wget \
    curl \
    unzip \
    ca-certificates \
    libx11-dev \
    libx264-dev \
    libfontconfig1 \
    libicu-dev \
    libjpeg62-turbo \
    libpng-dev \
    libnss3 \
    libxcomposite1 \
    libxdamage1 \
    libxrandr2 \
    libxext6 \
    libxi6 \
    libgdk-pixbuf2.0-0 \
    gnupg \
    lsb-release \
    && rm -rf /var/lib/apt/lists/* \
    # Adicionar chave GPG do Google Chrome de maneira mais segura
    && curl -sSL https://dl.google.com/linux/linux_signing_key.pub | tee /usr/share/keyrings/google-linux-signing-key.pub > /dev/null \
    # Adicionar o repositório do Google Chrome
    && DISTRO=$(lsb_release -c | awk '{print $2}') \
    && echo "deb [signed-by=/usr/share/keyrings/google-linux-signing-key.pub] https://dl.google.com/linux/chrome/deb/ stable main" | tee /etc/apt/sources.list.d/google-chrome.list \
    && apt-get update \
    # Instalar o Google Chrome
    && apt-get install -y google-chrome-stable \
    # Baixar e instalar o ChromeDriver
    && CHROME_DRIVER_VERSION=$(curl -sS chromedriver.storage.googleapis.com/LATEST_RELEASE) \
    && wget https://chromedriver.storage.googleapis.com/$CHROME_DRIVER_VERSION/chromedriver_linux64.zip \
    && unzip chromedriver_linux64.zip \
    && mv chromedriver /usr/local/bin/ \
    && rm chromedriver_linux64.zip

# Definir variáveis de ambiente para o Selenium
ENV CHROME_BIN=/usr/bin/google-chrome-stable
ENV CHROME_DRIVER=/usr/local/bin/chromedriver

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["GigaHouse.Worker.WebScraping/GigaHouse.Worker.WebScraping.csproj", "GigaHouse.Worker.WebScraping/"]
RUN dotnet restore "./GigaHouse.Worker.WebScraping/GigaHouse.Worker.WebScraping.csproj"
COPY . .

WORKDIR "/src/GigaHouse.Worker.WebScraping"
RUN dotnet build "./GigaHouse.Worker.WebScraping.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GigaHouse.Worker.WebScraping.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GigaHouse.Worker.WebScraping.dll"]