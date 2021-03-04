using MoXiang.Application.Request;
using MoXiang.Application.Response;
using MoXiang.Infrastructure.AutoMapper;
using MoXiang.Infrastructure.Returned;
using MoXiang.Repository.Dapper;
using MoXiang.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Application
{
    public class ArticleApp
    {
        private readonly IRepositoryBase _repositorybase;
        public ArticleApp(IRepositoryBase repositorybase)
        {
            _repositorybase = repositorybase;
        }
        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<TableData> Load(QueryArticleReq req) 
        {
            TableData data = new TableData();
            string sql = "select a.Id,a.Title,a.Banner,a.IsTop,a.IsHot,a.Summary,a.CreateTime,a.Hits,count(b.Id) commentsCount from Article a left join Comment b on b.ArticleId = a.Id";
            if (!string.IsNullOrWhiteSpace(req.Title))
            {
                sql +=$" where a.Title like '%@Title%' GROUP BY a.Id LIMIT {req.page*req.limit},@limit";
            }
            else 
            {
                sql += $" GROUP BY a.Id LIMIT {req.page*req.limit},@limit";
            }
            var articleList =await  _repositorybase.FindAsync<Article>(sql, req);
            var ArticleListResps = articleList.MapToList<ArticleListResp>();
            data.Data = ArticleListResps;
            data.Count = _repositorybase.Find<Article>("select id from Article").Count();
            return data;
        }
        /// <summary>
        /// 查看文章详情
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        public async Task<TableData> GetDetails(int ArticleId)
        {
            TableData data = new TableData();
            string articlesql = @"select a.Id,a.Title,a.Content,a.Hits,a.UpdateTime,b.Name TypeName
                            from Article a left join ArticleType b on a.TypeId = b.Id  where a.Id = @ArticleId; ";
            string commentssql = "select Id,Content,CreateTime,CreateUser,CommentId from `Comment` where ArticleId=@ArticleId;";
            var article =(await  _repositorybase.FindAsync<Article>(articlesql, new { ArticleId=ArticleId })).FirstOrDefault();
            var comments = await _repositorybase.FindAsync<Article>(commentssql, new { ArticleId = ArticleId });
            var details=article.MapTo<ArticleDetailsResp>();
            var commentsList = comments.MapToList<CommentsListResp>();
            details.CommentsList = commentsList;
            data.Data = details;
            return data;
        }
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task Add(AddOrUpDataArticleReq req)
        {
            string sql = "INSERT INTO Article(Title,Content,Bannerm,IsTop,IsHot,Summary,CreateTime,CreateUser,CreateUserId) VALUES(@Title,@Content,@Bannerm,@IsTop,@IsHot,@Summary,@CreateTime,@CreateUser,@CreateUserId)";
            var articleMap = req.MapTo<Article>();
            await _repositorybase.AddAsync<Article>(sql, articleMap);
        }
        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task UpData(AddOrUpDataArticleReq req)
        {
            throw new Exception("报错啦");
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        public async Task Delete(int ArticleId)
        {
            string sql = "delete from Article where id=@ArticleId";
            await _repositorybase.DeleteAsync<Article>(sql, ArticleId);
        }
    }
}
