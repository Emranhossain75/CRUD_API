using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrudApi.Models; 

namespace CrudApi.Controllers
{
    
    public class StudentInformationController : ApiController
    {
        CRUDEntities db = new CRUDEntities();

        public IHttpActionResult GetStudent()
        {
            var result = db.Student_Information.ToList();
            return Ok(result);
        }
        [Authorize]
        public IHttpActionResult CreateStudent(Student_Information student_Information)
        {
            db.Student_Information.Add(student_Information);
            db.SaveChanges();

            return Ok();
        }

        public IHttpActionResult GetDetails(int id)
        {
            Information informationDetails = null;
            informationDetails = db.Student_Information.Where(x => x.ID == id).Select(x => new Information()
            {
                ID = x.ID,
                FastName = x.FastName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                Description = x.Description
            }).FirstOrDefault<Information>();
            if(informationDetails == null)
            {
                return NotFound();
            }
            return Ok(informationDetails);
        }

        public IHttpActionResult Put(Information information)
        {
            var Updateinfo = db.Student_Information.Where(x => x.ID == information.ID).FirstOrDefault<Student_Information>();
            if(Updateinfo!= null)
            {
                Updateinfo.ID = information.ID;
                Updateinfo.FastName = information.FastName;
                Updateinfo.LastName = information.LastName;
                Updateinfo.Email = information.Email;
                Updateinfo.Phone = information.Phone;
                Updateinfo.Description = information.Description;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            var empdel = db.Student_Information.Where(x => x.ID == id).FirstOrDefault();
            db.Entry(empdel).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }
    }
}
