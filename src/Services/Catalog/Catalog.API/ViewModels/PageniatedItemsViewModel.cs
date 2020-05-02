using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.ViewModels
{
    public class PageniatedItemsViewModel<TEntity> where TEntity : class
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public long Count { get; set; }
        public IEnumerable<TEntity> Data { get; set; }

        public PageniatedItemsViewModel(int pageIndex,int pageSize,long count,IEnumerable<TEntity> data)
        {
            this.PageIndex = PageIndex;
            this.PageSize = PageSize;
            this.Count = count;
            this.Data = data;
        }
    }
}
