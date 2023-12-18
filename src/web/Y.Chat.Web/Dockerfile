# 定制的镜像都是基于 FROM 的镜像，这里将node:12.18.4设成基础镜像
# node后面跟的tag，可以在docker hub中（上方网址）查找项目所需的tag
FROM node:20.9.0 as build

COPY package*.json ./
RUN echo "COPY is success"

RUN yarn
RUN echo "install is success"

COPY . .

# 编译项目
RUN yarn build
RUN echo "build is success"


# 以nginx:1.12.2为基础镜像
# nginx后面跟的tag，可以在docker hub中（上方网址）查找项目所需的tag
FROM nginx:1.16.1

# 修改/usr/share/nginx/html里面的内容为前端需要部署的静态文件，这样前端就跑在nginx上了
WORKDIR /usr/share/nginx/html
RUN rm -f *
COPY --from=build /app/dist .

# 替换default.conf文件，解决单页面部署后刷新404问题
COPY --from=build /app/nginx-default.conf /etc/nginx/conf.d/default.conf