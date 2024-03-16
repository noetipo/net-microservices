FROM docker.elastic.co/apm/apm-server:8.5.2
USER root
COPY apm-server.yml /usr/share/apm-server/apm-server.yml
RUN chown -R apm-server /usr/share/apm-server/apm-server.yml
USER apm-server