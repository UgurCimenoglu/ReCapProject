using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {

            _colorDal = colorDal;
        }
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color entity)
        {
            _colorDal.Add(entity);
            return new SuccessResult("Renk Eklendi.");
        }

        public IResult Delete(Color entity)
        {
            _colorDal.Delete(entity);
            return new SuccessResult("Renk Silindi.");
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), "Renkler Listelendi.");
        }
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color entity)
        {
            _colorDal.Update(entity);
            return new SuccessResult("Renk Güncellendi.");
        }
    }
}
