using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    /// <summary>
    /// 新闻
    /// </summary>
    public class NewsService : CrudService<SysNews>, INewsService
    {
        public NewsService(IRepo<SysNews> repo)
            : base(repo)
        {

        }


        public List<SysNews> SelectBy(int isShow, string title, string sDate, string eDate, int pageIndex, ref int totalCount)
        {
            var result = this.mRepo.GetAll();
            if (isShow != -1) result = result.Where(m => m.IsShow == isShow);
            if (!string.IsNullOrEmpty(sDate)) result = result.Where(m => m.OccDate >= Convert.ToDateTime(sDate));
            if (!string.IsNullOrEmpty(eDate)) result = result.Where(m => m.OccDate < Convert.ToDateTime(eDate));
            totalCount = result.Count();
            var list = result.Take(pageIndex * AppGlobal.ManagerDataPageSize).OrderByDescending(m => m.OccDate).Skip(AppGlobal.ManagerDataPageSize * (pageIndex - 1)).ToList();
            return list;
        }

        public bool AddNews(SysNews model)
        {
            this.Create(model);
            this.Save();
            return true;
        }

        public bool DeleteNews(int id)
        {
            var entity = this.Get(id);
            entity.IsDelete = true;
            this.Save();
            return true;
        }

        public bool UpdateNews(SysNews model)
        {
            var entity = this.Get(model.Id);
            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.IsShow = model.IsShow;
            entity.OccDate = model.OccDate;
            this.Save();
            return true;
        }
    }
}
