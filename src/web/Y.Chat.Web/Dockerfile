# 定制的镜像都是基于 FROM 的镜像，这里将node:12.18.4设成基础镜像
# node后面跟的tag，可以在docker hub中（上方网址）查找项目所需的tag
FROM node:16.20.2-alpine as builder

# 设置项目目录
WORKDIR /app

COPY . .

# 还原 node_modules
RUN npm config get registry
RUN yarn 
RUN ls -l
RUN echo "package restore is success"

# 编译项目
# RUN npm run build
RUN yarn build
RUN echo "build is success"


FROM nginx:latest

# 修改/usr/share/nginx/html里面的内容为前端需要部署的静态文件，这样前端就跑在nginx上了
# 复制 nginx.conf 配置文件到镜像中
COPY ["nginx-default.conf", "/etc/nginx/nginx.conf"]


# 从编译镜像复制编译结果到此镜像
COPY --from=builder /app/dist /usr/share/nginx/html