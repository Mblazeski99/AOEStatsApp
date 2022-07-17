using Domain.Models;
using System;

namespace AOEStatsApp.ViewModels.DomainViewModels
{
    public abstract class DomainViewModelBase : ViewModelBase
    {
        private readonly BaseEntity _item;

        public DomainViewModelBase(BaseEntity item)
        {
            _item = item;
        }

        public int Id => _item.Id;
        public DateTime DateCreated => _item.DateCreated;
        public DateTime? DateModified => _item.DateModified;
    }
}
