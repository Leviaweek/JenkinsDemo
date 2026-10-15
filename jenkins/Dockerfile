FROM jenkins/jenkins:lts-jdk21

USER root

RUN apt-get update \
 && apt-get install -y --no-install-recommends curl ca-certificates libicu-dev git \
 && rm -rf /var/lib/apt/lists/*

# .NET SDK 10 внутри Jenkins-контейнера
ENV DOTNET_ROOT=/usr/share/dotnet
RUN curl -sSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh \
 && bash /tmp/dotnet-install.sh --channel 10.0 --install-dir "$DOTNET_ROOT" \
 && ln -s "$DOTNET_ROOT/dotnet" /usr/bin/dotnet \
 && rm /tmp/dotnet-install.sh

ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_NOLOGO=1

USER jenkins

# Нужные плагины (git, github, pipeline, junit и т.д.)
RUN jenkins-plugin-cli --plugins git github workflow-aggregator junit timestamper ws-cleanup
