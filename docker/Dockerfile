FROM alpine:3.16

RUN sed -i 's/dl-cdn.alpinelinux.org/mirrors.aliyun.com/g' /etc/apk/repositories

RUN apk update

# .Net dependency
RUN apk add --no-cache \
        ca-certificates \
        krb5-libs \
        libgcc \
        libintl \
        libssl1.1 \
        libstdc++ \
        zlib \
        tini \
        curl

COPY . /app
WORKDIR /app

ENTRYPOINT ["/sbin/tini", "--"]
CMD ["/app/app"]