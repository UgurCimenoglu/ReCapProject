using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Core.Utilities.Results;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        static string uploadPath = Environment.CurrentDirectory + @"\wwwroot\images\";
        public static string Add(IFormFile file)
        {

            var sourcepath = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = new FileStream(sourcepath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var result = newPath(file);
                File.Move(sourcepath, uploadPath + result);
                return result;
            }
            return null;
        }

        public static IResult Delete(string ImagePath)
        {
            try
            {
                File.Delete(ImagePath);
                return new SuccessResult("Silindi.");
            }
            catch (Exception)
            {

                return new ErrorResult();
            }
        }

        public static string Update(IFormFile file, string ImagePath)
        {
            try
            {
                var newImagePath = FileHelper.Add(file);
                if (newImagePath != null)
                {
                    File.Delete(ImagePath);
                    return newImagePath;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }


        }



        public static string newPath(IFormFile file)
        {
            //Yüklediğimiz resmin ismini alıyoruz.
            FileInfo fileInfo = new FileInfo(file.FileName);

            //Yüklediğimiz resmin uzantısını alıyoruz.(Örneğin .jpeg, .png vs)
            string extension = fileInfo.Extension;

            //Benzersiz bir string ifade oluşturuyorum.
            var newPath = Guid.NewGuid().ToString() + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + extension;

            //String ifadedeki /,: boşluk gib ifadeleri -'ye çeviriyorum.
            string unique = Regex.Replace(newPath, "[/|:| ]", "-");

            return unique;
        }
    }
}
