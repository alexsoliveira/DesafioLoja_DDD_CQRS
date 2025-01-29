FROM node:20 AS ng-builder
WORKDIR /app
COPY desafio.loja.client /app
RUN npm install
RUN npm install @angular/cli -g
EXPOSE 4200
CMD ["ng","serve","--host","0.0.0.0"]