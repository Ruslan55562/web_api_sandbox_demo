FROM maven:3-jdk-8-alpine AS build
ADD pom.xml /dmaven/
ADD src/ /dmaven/src/
WORKDIR /dmaven
RUN mvn -Dmaven.test.skip=true clean install

FROM tomcat:9.0.80-jre17-temurin-jammy
ARG TOMCAT_HOME=/usr/local/tomcat
USER root:root
COPY --from=build dmaven/target/parabank.war ${TOMCAT_HOME}/webapps

CMD ["catalina.sh", "run"]

EXPOSE 8080
#EXPOSE 61616
EXPOSE 9001