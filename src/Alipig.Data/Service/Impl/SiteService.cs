using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Data.Repositories;
using Alipig.Framework;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    public class SiteService : Disposable, ISiteService
    {
        private readonly ISiteRepository _siteRepository;

        public SiteService(ISiteRepository siteRepository)
        {
            this._siteRepository = siteRepository;
        }


        public IList<Site> GetAllSite()
        {
            return _siteRepository.GetAll();
        }

        public bool CreateSite(Site site)
        {
            var result = _siteRepository.Add(site);
            _siteRepository.Commit();
            return result;
        }


        public bool ModifySite(Site site)
        {
            var result = _siteRepository.EditById(site);
            _siteRepository.Commit();
            return result;
        }

        public Site GetSite(int id)
        {
            return _siteRepository.Get(id);
        }


        public void DeleteSite(int id)
        {
            var model = GetSite(id);
            _siteRepository.Delete(model.ID);
            _siteRepository.Commit();
        }


        public Site GetSite(Guid id)
        {
            return _siteRepository.Get(id);
        }


        public bool AddDomain(Guid id, string domain)
        {
            var model = _siteRepository.Get(id);
            if (String.IsNullOrEmpty(model.DomainList))
            {
                model.DomainList = domain;
            }
            else
            {
                model.DomainList = String.Format("{0},{1}", model.DomainList, domain);
            }
            var result = _siteRepository.EditById(model);
            _siteRepository.Commit();
            return result;
        }


        public bool IsDomainOnly(Guid id, string domain)
        {
            var model = _siteRepository.Get(id);
            if (model.DomainList.Split(',').Where(x => x == domain).Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
