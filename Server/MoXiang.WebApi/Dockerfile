
#基于 `microsoft/dotnet:5.0.0-core` 来构建我们的镜像

FROM mcr.microsoft.com/dotnet/sdk:5.0
#拷贝项目publish文件夹中的所有文件到 docker容器中的publish文件夹中 
COPY . /publish
 
#设置工作目录为 `/publish` 文件夹，即容器启动默认的文件夹
WORKDIR /publish
 
#设置Docker容器对外暴露端口
EXPOSE 9636
 
#使用`dotnet dev.dll`来运行应用程序，自定义运行端口
 
ENTRYPOINT ["dotnet", "MoXiang.WebApi.dll"]