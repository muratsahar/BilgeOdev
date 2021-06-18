using BilgeadamHomework.Models.EntityFramework;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeadamHomework.ViewModels;

namespace BilgeadamHomework.Controllers
{
    [Authorize]
    public class PersonelController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        
        public ActionResult Index()
        {
            var model = db.Personel.Include(x=>x.Departman).ToList();
            return View(model);
        }
        [Authorize(Roles ="A")]
        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel=new Personel()
            };
            return View("PersonelForm",model);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Personel personel)
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonelFormViewModel()
                {
                    Departmanlar = db.Departman.ToList(),
                    Personel = personel
                };
                return View("PersonelForm",model);
            }
            if (personel.Id==0)
            {
                db.Personel.Add(personel);//Yeni personel ekle işlemi
            }
            else//Güncelleme
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;//Tek satırda bütün bilgileri günceller.
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int Id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = db.Personel.Find(Id)
        };
            return View("PersonelForm",model);
        }

        public ActionResult Sil(int Id)
        {
            var silinecekPersonel = db.Personel.Find(Id);
            if (silinecekPersonel==null)
            {
                return HttpNotFound();
            }
            db.Personel.Remove(silinecekPersonel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelleriListele(int id)
        {
            var model = db.Personel.Where(x=>x.Id==id).ToList();
            return PartialView(model);
        }
    }
}
