using EMailApp.Business.Interfaces;
using EMailApp.Core.Concrete;
using EMailApp.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailApp.Business.Implementations
{
    public class DraftService : IDraftService
    {

        readonly IDraftRepository _draftRepository;
        public DraftService(IDraftRepository draftRepository)
        {
            _draftRepository=draftRepository;
        }

        public void TDelete(int id)
        {
            _draftRepository.Delete(id);
        }

        public Draft TGetById(int id)
        {
           return _draftRepository.GetById(id);
        }

        public List<Draft> TGetListAll()
        {
            return _draftRepository.GetList();
        }

        public void TInsert(Draft t)
        {
            _draftRepository.Insert(t);
        }

        public void TUpdate(Draft t)
        {
            _draftRepository.Update(t);
        }
    }
}
