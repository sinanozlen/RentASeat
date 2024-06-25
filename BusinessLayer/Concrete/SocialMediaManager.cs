using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class SocialMediaManager : ISocialMediaService
    {
        private readonly ISocialMediaDal _socialMediaDal;

        public SocialMediaManager(ISocialMediaDal socialMediaDal)
        {
            _socialMediaDal = socialMediaDal;
        }

        public void TAdd(SocialMedia entity)
        {
            _socialMediaDal.Add(entity);
            
        }

        public void TDelete(SocialMedia entity)
        {
            _socialMediaDal.Delete(entity);
        }

        public SocialMedia TGetbyID(int ID)
        {
           var values= _socialMediaDal.GetbyID(ID);
            return values;
        }

        public List<SocialMedia> TGetListAll()
        {
            var values=_socialMediaDal.GetListAll();
            return values;
        }

        public void TUpdate(SocialMedia entity)
        {
            _socialMediaDal.Update(entity);
        }
    }
}
