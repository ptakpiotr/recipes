FROM node:lts-alpine as build-stage
WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build

FROM node:lts-alpine as production-stage
WORKDIR /app

COPY --from=build-stage /app/dist ./dist
COPY --from=build-stage /app/package*.json ./

RUN npm install --only=production

EXPOSE 3000

CMD ["node", "dist/main.js"]