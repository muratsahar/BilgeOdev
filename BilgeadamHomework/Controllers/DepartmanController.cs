using BilgeadamHomework.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BilgeadamHomework.Controllers
{
    
    public class DepartmanController : Controller
    {
        PersonelDbEntities db = new PersonelDbEntities();
        
        public ActionResult Index()//İlk açılış ekranı. Tüm departmanlar gösterilecek.
        {
            var model= db.Departman.ToList();//Deprtman tablosunu listeye çevirdim.
            return View(model);//Listeyi viewe gönderdim.
        }
        [HttpGet]//Default değer.
        [Authorize(Roles = "A")]
        public ActionResult Yeni()//Yeni Departman için boş form gönderir. 
        {
            return View("DepartmanForm",new Departman());
        }
        //[HttpPost]Post durumunda bu aksiyon çalışsın.Antiforgery için pasife aldım.
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Departman departman)//Yeni departman formundan dönen bilgiyi db'e kaydeder. 
        {
            if (!ModelState.IsValid)//server side Validation
            {
                return View("DepartmanForm");
            } 
            if (departman.Id==0)//id=0 ise yeni ekleme kaydı
            {
                db.Departman.Add(departman);
            }else//id!=0 ise güncelleme kaydı
            {
                var guncellenecekDepartman = db.Departman.Find(departman.Id);
                if (guncellenecekDepartman==null)
                {
                    return HttpNotFound();
                }
                guncellenecekDepartman.Ad = departman.Ad;
            }
            db.SaveChanges();
            return RedirectToAction("Index","Departman");
        }
       
        public ActionResult Guncelle(int Id)//argüman olarak Id alacak.DepartmanForm view'i ortak kullanılabilir. 
        {
            var model = db.Departman.Find(Id);
            if (model == null)
                return HttpNotFound();
            return View("DepartmanForm",model);//Güncelle view yerine DepartmanForm view'e gönder.
        }
        public ActionResult Sil(int Id)
        {
            var silinecek = db.Departman.Find(Id);
            if (silinecek==null)
            {
                return HttpNotFound();
            }
            db.Departman.Remove(silinecek);
            db.SaveChanges();
            return RedirectToAction("Index","Departman");
        }
    }
}