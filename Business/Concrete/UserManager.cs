using Business.Abstract;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User entity)
        {
            _userDal.Add(entity);
            return new SuccessResult("Kullanici Eklendi.");
        }

        public IResult Delete(User entity)
        {
            _userDal.Delete(entity);
            return new SuccessResult("Kullanici Silindi.");
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), "Kullanicilar Listelendi.");
        }

        public IResult Update(User entity)
        {
            _userDal.Update(entity);
            return new SuccessResult("Kullanici Güncellendi.");
        }
    }
}
