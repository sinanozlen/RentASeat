using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class FeatureManager : IFeatureService
    {
        private readonly IFeatureDal _featureDal;

        public FeatureManager(IFeatureDal featureDal)
        {
            _featureDal = featureDal;
        }

        public void TAdd(Feature entity)
        {
            _featureDal.Add(entity);
        }

        public void TDelete(Feature entity)
        {
            _featureDal.Delete(entity);
        }

        public Feature TGetbyID(int ID)
        {
            var values=_featureDal.GetbyID(ID);
            return values;
        }

        public List<Feature> TGetListAll()
        {
            var values=_featureDal.GetListAll();
            return values;
        }

        public void TUpdate(Feature entity)
        {
            _featureDal.Update(entity);
        }
    }
}
