# 设置基础镜像，这里使用最新的nginx镜像，前面已经拉取过了
FROM nginx
# 定义作者 Moxiang
MAINTAINER Moxiang 
# 将dist文件中的内容复制到 /usr/share/nginx/html/ 这个目录下面
COPY dist/ /usr/share/nginx/html/
COPY nginx/default.conf /etc/nginx/conf.d/default.conf