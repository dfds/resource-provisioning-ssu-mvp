FROM alpine:latest

RUN apk update && apk add libstdc++ && apk add libintl && apk add icu

WORKDIR /app
COPY ./SsuCli-linux-musl-x64 ./cli
RUN chmod +x cli

ENTRYPOINT [ "/app/cli" ]