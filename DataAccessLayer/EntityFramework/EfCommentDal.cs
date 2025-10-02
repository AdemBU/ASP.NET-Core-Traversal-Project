using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        public List<Comment> GetListCommentWithDestination()
        {
            using (var c = new Context())  //EF Core DbContext örneği oluşturuluyor. using bloğu sayesinde işlem bitince bellekten atılır.
            {
                return c.Comments.Include(x => x.Destination).ToList(); //Her yorumla birlikte ilişkili Destination bilgileri de yüklensin demektir(Eager Loading yapılır).
            }
        }

        public List<Comment> GetListCommentWithDestinationAndUser(int id)
        {
            using (var c = new Context())  //EF Core DbContext örneği oluşturuluyor. using bloğu sayesinde işlem bitince bellekten atılır.
            {
                return c.Comments.Where(x => x.DestinationID == id).Include(x => x.AppUser).ToList(); //Her yorumla birlikte ilişkili AppUser bilgileri de yüklensin demektir(Eager Loading yapılır).
            }
        }
    }
}
