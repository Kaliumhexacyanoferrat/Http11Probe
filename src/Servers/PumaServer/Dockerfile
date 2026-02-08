FROM ruby:3.3-slim
RUN apt-get update && apt-get install -y --no-install-recommends build-essential && \
    gem install puma --no-doc && \
    apt-get purge -y build-essential && apt-get autoremove -y && \
    rm -rf /var/lib/apt/lists/*
COPY src/Servers/PumaServer/config.ru /app/config.ru
WORKDIR /app
EXPOSE 8080
CMD ["puma", "-b", "tcp://0.0.0.0:8080"]
